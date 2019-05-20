using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace RemoteService
{
    public struct Configuration
    {
        public string serverAddress;
        public int serverPort;
        public int reconnectInterval;
        public string machineID;

        public Configuration(string address, int port, int interval, string machineID)
        {
            serverAddress = address;
            serverPort = port;
            reconnectInterval = interval;
            this.machineID = machineID;
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
            try
            {
                InitializeComponent();
                System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
                configPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "config.xml";

                System.Xml.Serialization.XmlSerializer serial = new System.Xml.Serialization.XmlSerializer(typeof(Configuration));
                _client = new TcpComm.Client(update, true, 30);
                if (!File.Exists(configPath))
                {
                    StreamWriter sw = new StreamWriter(File.Create(configPath));
                    Configuration cfgTemplate = new Configuration();
                    _isInitialized = true;
                    cfgTemplate.serverAddress = _client.GetLocalIpAddress().ToString();
                    cfgTemplate.serverPort = 5010;
                    cfgTemplate.reconnectInterval = 30;
                    // Assign random MachineID if none is specificied explicitly
                    cfgTemplate.machineID = new Random().Next(0, 255).ToString();
                    serial.Serialize(sw, cfgTemplate);
                    _config = cfgTemplate;
                }
                else
                {
                    StreamReader sr = new StreamReader(configPath);
                    _config = (Configuration)serial.Deserialize(sr);
                }

                string errMsg = "";
                _isConnected = _client.Connect(_config.serverAddress, Convert.ToUInt16(_config.serverPort), _config.machineID, ref errMsg);

                string commonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                string rcvFilesDir = Path.Combine(commonAppData, "MEUService");
                if(!Directory.Exists(rcvFilesDir))
                {
                    Directory.CreateDirectory(rcvFilesDir);
                }

                _receivedFilesPath = rcvFilesDir;
                _client.SetReceivedFilesFolder(_receivedFilesPath);
            }
            catch (Exception)
            {
            }
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
                _client.SetMachineID(_config.machineID);
                while (!_shutdownEvents.WaitOne(0))
                {
                    try
                    {
                        if (!_isInitialized)
                        {
                            _client = new TcpComm.Client(update, true, 30);
                            _isInitialized = true;
                        }

                        if (!_isConnected)
                        {
                            string errMsg = "";
                            _isConnected = _client.Connect(_config.serverAddress, Convert.ToUInt16(_config.serverPort), _config.machineID, ref errMsg);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void update(byte[] bytes, byte dataChannel)
        {
            try
            {
                bool dontReport = false;
                if (dataChannel < 251)
                {
                    // Try to execute command and send reply to caller
                    if (bytes.Length < 65535)
                    {
                        bool isCmdCorrect = tryExecuteCommand(TcpComm.Utilities.BytesToString(bytes));
                        //if (!isCmdCorrect)
                        //{
                        //    string errMsg = "";
                        //    _client.SendText("SVR_OK", 1, ref errMsg);
                        //}
                        //else
                        //{
                        //    string errMsg = "";
                        //    _client.SendText(msg, 1, ref errMsg);
                        //}
                    }
                }
                else if (dataChannel == 255)
                {
                    String msg = TcpComm.Utilities.BytesToString(bytes);
                    String tmp = "";

                    // Receiving file in progress
                    if (msg.Length > 15)
                    {
                        tmp = msg.Substring(0, 15);
                        if (tmp == "Receiving file:")
                        {
                            this.EventLog.WriteEntry(msg + " " + _client.GetIncomingFileName());
                            dontReport = true;
                        }
                    }

                    // Sending file in progress
                    if (msg.Length > 13)
                    {
                        tmp = msg.Substring(0, 13);
                        if (tmp == "Sending file:")
                        {
                            this.EventLog.WriteEntry(msg + " " + _client.GetOutgoingFileName());
                            dontReport = true;
                        }
                    }

                    // Completed file receiving
                    if (msg == "->Done")
                    {
                        string errMsg = "";
                        _client.SendText(_client.GetMachineID() + ":FILE_RECEIVED", errMsg: ref errMsg);
                        this.EventLog.WriteEntry(msg);
                        dontReport = true;
                    }

                    //Completed file sending
                    if (msg == "<-Done")
                    {
                        this.EventLog.WriteEntry(msg);
                        dontReport = true;
                    }

                    // Aborted file receiving
                    if (msg == "->Aborted")
                    {
                        string errMsg = "";
                        _client.SendText(_client.GetMachineID() + ":FILE_RECEIVING_ABORTED", errMsg: ref errMsg);
                        this.EventLog.WriteEntry(msg);
                        dontReport = true;
                    }

                    // Aborted file sending
                    if (msg == "<-Aborted")
                    {
                        this.EventLog.WriteEntry(msg);
                        dontReport = true;
                    }

                    // UBS = User Bytes Sent on channel :???
                    if (msg.Length > 4)
                    {
                        tmp = msg.Substring(0, 4);
                        if (tmp == "UBS:")
                        {
                            this.EventLog.WriteEntry(msg);
                            dontReport = true;
                        }
                    }

                    // An error occured
                    if (msg.Length > 4)
                    {
                        tmp = msg.Substring(0, 5);
                        if (tmp == "ERR: ")
                        {
                            this.EventLog.WriteEntry("Wystąpił błąd: " + msg);
                            dontReport = true;
                        }
                    }

                    // Server has disconnected (attempt to reconnect)
                    if (msg.Equals("Disconnected."))
                    {
                        _isConnected = false;
                        dontReport = true;
                    }

                    // Unknown message
                    if (!dontReport)
                    {
                        this.EventLog.WriteEntry("Nieznany typ wiadomości: " + msg);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private bool tryExecuteCommand(string cmd)
        {
            try
            {
                bool isCorrect = false;
                string machineID = _config.machineID;
                List<string> commands = ServerCommands.GetCommandList();
                string cmdToExecute = commands.FirstOrDefault(cmd.ToUpper().Contains).ToUpper();
                if (cmdToExecute.Length == 0)
                {
                    string errMsg = "";
                    _client.SendText(machineID + ":SVR_CMD_ZERO_LENGTH", errMsg: ref errMsg);
                    return isCorrect;
                }

                if (cmdToExecute == ServerCommands.SVR_SHUTDOWN)
                {
                    string errMsg = "";
                    _client.SendText(machineID + ":SVR_SHUTDOWN_OK", errMsg: ref errMsg);
                    CommonActions.Shutdown();
                    isCorrect = true;
                }
                else if (cmdToExecute == ServerCommands.SVR_RESTART)
                {
                    string errMsg = "";
                    _client.SendText(machineID + ":SVR_RESTART_OK", errMsg: ref errMsg);
                    CommonActions.Shutdown("/r /f /t 0");
                    isCorrect = true;
                }
                else if (cmdToExecute == ServerCommands.SVR_LOGOFF)
                {
                    CommonActionsLogout.LogoutUsers();
                    string errMsg = "";
                    _client.SendText(machineID + ":SVR_LOGOFF_OK", errMsg: ref errMsg);
                    isCorrect = true;
                }
                else if (cmdToExecute == ServerCommands.SVR_LOGOUT)
                {
                    CommonActionsLogout.LogoutUsers();
                    string errMsg = "";
                    _client.SendText(machineID + ":SVR_LOGOUT_OK", errMsg: ref errMsg);
                    isCorrect = true;
                }
                else if (cmdToExecute == ServerCommands.SVR_RUN)
                {
                    try
                    {
                        string[] param = cmd.Substring(3).Split(' ');
                        string path = param[0];
                        string args = "";
                        foreach (string arg in param)
                        {
                            args += arg + " ";
                        }
                        args.TrimEnd();
                        bool isRcvFilesFolder = false;
                        DirectoryInfo info = new DirectoryInfo(_receivedFilesPath);
                        foreach (var item in info.GetFiles())
                        {
                            if(item.Name == path)
                            {
                                isRcvFilesFolder = true;
                                break;
                            }
                        }

                        path = isRcvFilesFolder ? Path.Combine(_receivedFilesPath, path) : path;

                        try
                        {
                            CommonActionsRunAs.RunExecutable(path, args);
                            string errMsg = "";
                            _client.SendText(machineID + ":SVR_RUN_OK", errMsg: ref errMsg);
                        }
                        catch (Exception ex)
                        {
                            string errMsg = "";
                            _client.SendText(machineID + ex.Message, errMsg: ref errMsg);
                        }
                        //bool isRun = CommonActions.RunExecutable(filepath, _receivedFilesPath);
                        //if (isRun)
                        //{
                        //    string errMsg = "";
                        //    _client.SendText(machineID + ":SVR_RUN_OK", errMsg: ref errMsg);
                        //}
                        //else
                        //{
                        //    string errMsg = "";
                        //    _client.SendText(machineID + ":SVR_RUN_ERROR", errMsg: ref errMsg);
                        //}
                    }
                    catch (Exception ex)
                    {
                        string errMsg = "";
                        _client.SendText(machineID + ex.Message, errMsg: ref errMsg);
                    }

                    isCorrect = true;
                }
                else if (cmdToExecute == ServerCommands.SVR_CHECKPROCESS)
                {
                    string procName = cmd.Substring(12);
                    try
                    {
                        bool isRunning = CommonActions.CheckProcessRunning(procName);
                        if(isRunning)
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
                else if (cmdToExecute == ServerCommands.SVR_KILLPROCESS)
                {
                    string procName = cmd.Substring(11).Trim();
                    try
                    {
                        bool isKilled = CommonActions.KillProcess(procName);
                        if(isKilled)
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
                string errMsg = "";
                _client.SendText(_config.machineID + ":SVR_ERROR", errMsg: ref errMsg);
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
