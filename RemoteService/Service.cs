using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private TcpComm.Client _client;
        private string configPath;
        private Configuration _config;
        private bool _isInitialized = false;
        private bool _isConnected = false;
        private Thread _thread;
        private ManualResetEvent _shutdownEvents = new ManualResetEvent(false);
        private string _receivedFilesPath;

        /// <summary>
        /// Just to keep the service alive
        /// </summary>
        private Timer _timer;
        #endregion

        public MEUService()
        {
            InitializeComponent();
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            configPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "config.xml";

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Configuration));
            _client = new TcpComm.Client(Update, true, 30);
            if (!File.Exists(configPath))
            {
                StreamWriter sw = new StreamWriter(File.Create(configPath));
                Configuration cfgTemplate = new Configuration();
                _isInitialized = true;
                cfgTemplate.ServerAddress = _client.GetLocalIpAddress().ToString();
                cfgTemplate.ServerPort = 5010;
                cfgTemplate.ReconnectInterval = 30;
                // Assign random MachineID if none is specificied explicitly
                cfgTemplate.MachineId = new Random().Next(0, 255).ToString();
                serializer.Serialize(sw, cfgTemplate);
                _config = cfgTemplate;
            }
            else
            {
                StreamReader sr = new StreamReader(configPath);
                _config = (Configuration)serializer.Deserialize(sr);
            }

            string errMsg = string.Empty;
            _isConnected = _client.Connect(_config.ServerAddress, Convert.ToUInt16(_config.ServerPort), _config.MachineId, ref errMsg);

            string commonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string receivedFilesFolderPath = Path.Combine(commonAppData, "MEUService");
            if (!Directory.Exists(receivedFilesFolderPath))
            {
                Directory.CreateDirectory(receivedFilesFolderPath);
            }

            _receivedFilesPath = receivedFilesFolderPath;
            _client.SetReceivedFilesFolder(_receivedFilesPath);
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _thread = new Thread(DoWork);
                _thread.IsBackground = true;
                _thread.Start();
                this.scheduleTask();
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
            try
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
            catch
            {
                // ignored
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
                        bool isCmdCorrect = TryExecuteCommand(TcpComm.Utilities.BytesToString(bytes));
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

        private bool TryExecuteCommand(string cmd)
        {
            try
            {
                bool isCorrect = false;
                string machineID = _config.MachineId;
                List<string> commands = ServerCommands.GetCommandsList();
                string cmdToExecute = commands.First(cmd.ToUpper().Contains).ToUpper();
                if (cmdToExecute.Length == 0)
                {
                    string errMsg = string.Empty;
                    _client.SendText(machineID + ":SVR_CMD_ZERO_LENGTH", errMsg: ref errMsg);
                }
                else if (cmdToExecute == ServerCommands.SvrShutdown)
                {
                    string errMsg = "";
                    _client.SendText(machineID + ":SVR_SHUTDOWN_OK", errMsg: ref errMsg);
                    CommonActions.Shutdown();
                    isCorrect = true;
                }
                else if (cmdToExecute == ServerCommands.SvrRestart)
                {
                    string errMsg = "";
                    _client.SendText(machineID + ":SVR_RESTART_OK", errMsg: ref errMsg);
                    CommonActions.Shutdown("/r /f /t 0");
                    isCorrect = true;
                }
                else if (cmdToExecute == ServerCommands.SvrLogoff)
                {
                    CommonActions.LogoutUsers();
                    string errMsg = "";
                    _client.SendText(machineID + ":SVR_LOGOFF_OK", errMsg: ref errMsg);
                    isCorrect = true;
                }
                else if (cmdToExecute == ServerCommands.SvrLogout)
                {
                    CommonActions.LogoutUsers();
                    string errMsg = "";
                    _client.SendText(machineID + ":SVR_LOGOUT_OK", errMsg: ref errMsg);
                    isCorrect = true;
                }
                else if (cmdToExecute == ServerCommands.SvrRun)
                {
                    try
                    {
                        string[] parameters = cmd.Substring(3).Split(' ');
                        string path = parameters[0];
                        string args = parameters.Aggregate((acc, arg) => $"{acc} {arg}");
                        foreach (string arg in parameters)
                        {
                            args += arg + " ";
                        }
                        args.TrimEnd();
                        bool isReceivedFilesFolder = false;
                        DirectoryInfo info = new DirectoryInfo(_receivedFilesPath);
                        foreach (var item in info.GetFiles())
                        {
                            if (item.Name == path)
                            {
                                isReceivedFilesFolder = true;
                                break;
                            }
                        }

                        path = isReceivedFilesFolder ? Path.Combine(_receivedFilesPath, path) : path;

                        try
                        {
                            CommonActions.RunExecutable(path, args);
                            string errMsg = string.Empty;
                            _client.SendText(machineID + ":SVR_RUN_OK", errMsg: ref errMsg);
                        }
                        catch (Exception ex)
                        {
                            string errMsg = string.Empty;
                            _client.SendText(machineID + ex.Message, errMsg: ref errMsg);
                        }
                    }
                    catch (Exception ex)
                    {
                        string errMsg = "";
                        _client.SendText(machineID + ex.Message, errMsg: ref errMsg);
                    }

                    isCorrect = true;
                }
                else if (cmdToExecute == ServerCommands.SvrCheckprocess)
                {
                    string procName = cmd.Substring(12);
                    try
                    {
                        bool isRunning = CommonActions.CheckProcessRunning(procName);
                        if (isRunning)
                        {
                            string errMsg = "";
                            _client.SendText(machineID + ":SVR_CHECK_PROCESS_GOOD", errMsg: ref errMsg);
                        }
                        else
                        {
                            string errMsg = "";
                            _client.SendText(machineID + ":SVR_CHECK_PROCESS_BAD", errMsg: ref errMsg);
                        }
                    }
                    catch (Exception ex)
                    {
                        string errMsg = "";
                        _client.SendText(machineID + ex.Message, errMsg: ref errMsg);
                    }
                }
                else if (cmdToExecute == ServerCommands.SvrKillprocess)
                {
                    string procName = cmd.Substring(11).Trim();
                    try
                    {
                        bool isKilled = CommonActions.KillProcess(procName);
                        if (isKilled)
                        {
                            string errMsg = "";
                            _client.SendText(machineID + ":SVR_KILL_PROCESS_OK", errMsg: ref errMsg);
                        }
                        else
                        {
                            string errMsg = "";
                            _client.SendText(machineID + ":SVR_KILL_PROCESS_FAIL", errMsg: ref errMsg);
                        }
                    }
                    catch (Exception ex)
                    {
                        string errMsg = "";
                        _client.SendText(machineID + ex.Message, errMsg: ref errMsg);
                    }
                    isCorrect = true;
                }
                else
                {
                    string errMsg = "";
                    _client.SendText(machineID + ":SVR_UNKNOWN_COMMAND", errMsg: ref errMsg);
                    isCorrect = false;
                }

                return isCorrect;
            }
            catch (Exception)
            {
                string errMsg = string.Empty;
                _client.SendText(_config.MachineId + ":SVR_ERROR", errMsg: ref errMsg);
                return false;
            }
        }

        private void keepServiceAlive(object sender)
        {
            try
            {
                Thread.Sleep(1000);
                this.scheduleTask();
            }
            catch (Exception)
            {
            }
        }

        private void scheduleTask()
        {
            try
            {
                _timer = new Timer(new TimerCallback(keepServiceAlive));
                DateTime scheduleTime = DateTime.MinValue;

                int interval = 5;
                scheduleTime = DateTime.Now.AddMinutes(interval);

                if (DateTime.Now > scheduleTime)
                {
                    scheduleTime.AddMinutes(interval);
                }

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
