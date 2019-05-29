using RemoteService.CommandHandlers;
using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace RemoteService
{
    public struct Configuration
    {
        public string ServerAddress { get; set; }
        public int ServerPort { get; set; }
        public int ReconnectInterval { get; set; }
        public string MachineId { get; set; }

        public Configuration(string address, int port, int interval, string machineID)
        {
            ServerAddress = address;
            ServerPort = port;
            ReconnectInterval = interval;
            this.MachineId = machineID;
        }
    }

    public partial class MEUService : ServiceBase
    {
        #region Fields
        private const string DefaultConfigFileName = "config.xml";
        private const int DefaultServerPort = 5010;
        private const int DefaultReconnectInterval = 30;
        private const string DefaultReceivedFilesFolderName = "MEUService";
        private const int MaxMachineId = 255;
        private const int DefaultKeepAliveMillisInterval = 1000;
        private const int DefaultTaskScheduleMinutesInterval = 5;

        private TcpComm.Client _client;
        private readonly string _configPath;
        private readonly Configuration _config;
        private bool _isInitialized = false;
        private bool _isConnected = false;
        private Thread _thread;
        private ManualResetEvent _shutdownEvents = new ManualResetEvent(false);
        private string _receivedFilesPath;
        private ICommandHandler _commandHandler;

        /// <summary>
        /// Just to keep the service alive
        /// </summary>
        private Timer _timer;
        #endregion

        public MEUService()
        {
            InitializeComponent();
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            _configPath = Path.Combine(Environment.CurrentDirectory, DefaultConfigFileName);

            InitializeDirectories();
            _client = new TcpComm.Client(Update, true, 30);
            _config = InitializeConfiguration();

            string errMsg = string.Empty;

            _isConnected = _client.Connect(_config.ServerAddress, Convert.ToUInt16(_config.ServerPort), _config.MachineId, ref errMsg);
            _client.SetReceivedFilesFolder(_receivedFilesPath);

            InitializeCommandHandlers();
        }

        private void InitializeCommandHandlers()
        {
            var zeroLengthHandler = new ZeroLengthCommandHandler();
            var shutdownHandler = new ShutdownCommandHandler();
            var restartHandler = new RestartCommandHandler();
            var logoutHandler = new LogOutCommandHandler();
            var checkProcessHandler = new CheckProcessCommandHandler();
            var killProcessHandler = new KillProcessCommandHandler();
            var runHandler = new RunCommandHandler(_receivedFilesPath);

            _commandHandler = zeroLengthHandler.SetNextHandler(shutdownHandler)
                .SetNextHandler(restartHandler).SetNextHandler(logoutHandler)
                .SetNextHandler(checkProcessHandler).SetNextHandler(killProcessHandler)
                .SetNextHandler(runHandler);
        }

        private void InitializeDirectories()
        {
            string commonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string receivedFilesFolderPath = Path.Combine(commonAppData, DefaultReceivedFilesFolderName);
            if (!Directory.Exists(receivedFilesFolderPath))
            {
                Directory.CreateDirectory(receivedFilesFolderPath);
            }
            _receivedFilesPath = receivedFilesFolderPath;
        }

        private Configuration InitializeConfiguration()
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Configuration));
            if (!File.Exists(_configPath))
            {
                StreamWriter sw = new StreamWriter(File.Create(_configPath));
                Configuration cfgTemplate = new Configuration();
                _isInitialized = true;
                cfgTemplate.ServerAddress = _client.GetLocalIpAddress().ToString();
                cfgTemplate.ServerPort = DefaultServerPort;
                cfgTemplate.ReconnectInterval = DefaultReconnectInterval;
                // Assign random MachineID if none is specificied explicitly
                cfgTemplate.MachineId = new Random().Next(0, MaxMachineId).ToString();
                serializer.Serialize(sw, cfgTemplate);
                return cfgTemplate;
            }
            else
            {
                StreamReader sr = new StreamReader(_configPath);
                return (Configuration)serializer.Deserialize(sr);
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _thread = new Thread(DoWork) { IsBackground = true };
                _thread.Start();
                this.ScheduleTask();
            }
            catch (Exception)
            {
            }
        }

        protected override void OnStop()
        {
            try
            {
                _shutdownEvents.Set();
                if (!_thread.Join(3000))
                {
                    _thread.Abort();
                    _client.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        private void DoWork()
        {
            _client.SetMachineID(_config.MachineId);
            while (!_shutdownEvents.WaitOne(0))
            {
                try
                {
                    if (!_isInitialized)
                    {
                        _client = new TcpComm.Client(Update, true, 30);
                        _isInitialized = true;
                    }

                    if (!_isConnected)
                    {
                        string errMsg = string.Empty;
                        _isConnected = _client.Connect(_config.ServerAddress,
                            Convert.ToUInt16(_config.ServerPort),
                            _config.MachineId, ref errMsg);
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }

        private void Update(byte[] bytes, byte dataChannel)
        {
            try
            {
                bool dontReport = false;
                if (dataChannel < 251)
                {
                    // Try to execute command and send reply to caller
                    if (bytes.Length < 65535)
                    {
                        TryExecuteCommand(TcpComm.Utilities.BytesToString(bytes));
                    }
                }
                else if (dataChannel == 255)
                {
                    String message = TcpComm.Utilities.BytesToString(bytes);
                    String tmp = string.Empty;

                    // Receiving file in progress
                    if (message.Length > 15)
                    {
                        tmp = message.Substring(0, 15);
                        if (tmp == "Receiving file:")
                        {
                            this.EventLog.WriteEntry(message + " " + _client.GetIncomingFileName());
                            dontReport = true;
                        }
                    }

                    // Sending file in progress
                    if (message.Length > 13)
                    {
                        tmp = message.Substring(0, 13);
                        if (tmp == "Sending file:")
                        {
                            this.EventLog.WriteEntry(message + " " + _client.GetOutgoingFileName());
                            dontReport = true;
                        }
                    }

                    // Completed file receiving
                    if (message == "->Done")
                    {
                        string errMsg = "";
                        _client.SendText(_client.GetMachineID() + ":FILE_RECEIVED", errMsg: ref errMsg);
                        this.EventLog.WriteEntry(message);
                        dontReport = true;
                    }

                    //Completed file sending
                    if (message == "<-Done")
                    {
                        this.EventLog.WriteEntry(message);
                        dontReport = true;
                    }

                    // Aborted file receiving
                    if (message == "->Aborted")
                    {
                        string errMsg = "";
                        _client.SendText(_client.GetMachineID() + ":FILE_RECEIVING_ABORTED", errMsg: ref errMsg);
                        this.EventLog.WriteEntry(message);
                        dontReport = true;
                    }

                    // Aborted file sending
                    if (message == "<-Aborted")
                    {
                        this.EventLog.WriteEntry(message);
                        dontReport = true;
                    }

                    // UBS = User Bytes Sent on channel :???
                    if (message.Length > 4)
                    {
                        tmp = message.Substring(0, 4);
                        if (tmp == "UBS:")
                        {
                            this.EventLog.WriteEntry(message);
                            dontReport = true;
                        }
                    }

                    // An error occured
                    if (message.Length > 4)
                    {
                        tmp = message.Substring(0, 5);
                        if (tmp == "ERR: ")
                        {
                            this.EventLog.WriteEntry("Wystąpił błąd: " + message);
                            dontReport = true;
                        }
                    }

                    // Server has disconnected (attempt to reconnect)
                    if (message.Equals("Disconnected."))
                    {
                        _isConnected = false;
                        dontReport = true;
                    }

                    // Unknown message
                    if (!dontReport)
                    {
                        this.EventLog.WriteEntry("Nieznany typ wiadomości: " + message);
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        private bool TryExecuteCommand(string command)
        {
            bool isCorrect = false;

            try
            {
                string machineId = _config.MachineId;
                string result = _commandHandler.Handle(command);
                string message;

                if (string.IsNullOrEmpty(result))
                {
                    message = machineId + ":SVR_UNKNOWN_COMMAND";
                }
                else
                {
                    message = machineId + result;
                    isCorrect = true;
                }

                string errMsg = string.Empty;
                _client.SendText(message, errMsg: ref errMsg);
            }
            catch
            {
                string errMsg = string.Empty;
                _client.SendText(_config.MachineId + ":SVR_ERROR", errMsg: ref errMsg);
                isCorrect = false;
            }

            return isCorrect;
        }

        private void KeepServiceAlive(object sender)
        {
            try
            {
                Thread.Sleep(DefaultKeepAliveMillisInterval);
                this.ScheduleTask();
            }
            catch (Exception)
            {
            }
        }

        private void ScheduleTask()
        {
            try
            {
                _timer = new Timer(new TimerCallback(KeepServiceAlive));
                DateTime scheduleTime = DateTime.Now.AddMinutes(DefaultTaskScheduleMinutesInterval);
                TimeSpan span = scheduleTime.Subtract(DateTime.Now);
                int dueTime = Convert.ToInt32(span.TotalMilliseconds);
                _timer.Change(dueTime, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                this.EventLog.WriteEntry("Wystąpił błąd podczas działania usługi - " + ex.Message);
            }
        }
    }
}
