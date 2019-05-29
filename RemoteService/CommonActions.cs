using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using Cassia;

namespace RemoteService
{
    public static class CommonActions
    {
        private const string DefaultShutdownParameters = "/s /f /t 0";
        private static readonly ITerminalServicesManager _manager = new TerminalServicesManager();

        /// <summary>
        /// Gets host local IP Address
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocalIP()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        }

        /// <summary>
        /// Shutdowns host with specified parameters
        /// </summary>
        /// <param name="arguments">System shutdown parameters. Defaults to: /s /f /t 0</param>
        public static bool Shutdown(string arguments = DefaultShutdownParameters)
        {
            try
            {
                string options = arguments ?? DefaultShutdownParameters;
                ProcessStartInfo proc = new ProcessStartInfo("shutdown", options)
                {
                    CreateNoWindow = true, 
                    UseShellExecute = false
                };
                Process.Start(proc);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Check if specified process is running
        /// </summary>
        /// <param name="processName">Name of the process to check</param>
        /// <returns></returns>
        public static bool CheckProcessRunning(string processName)
        {
            try
            {
                int occurances = 0;
                Process[] processes = Process.GetProcesses();
                foreach (var process in processes)
                {
                    if(process.ProcessName.Contains(processName))
                    {
                        occurances++;
                    }
                }

                return occurances != 0;
            }
            catch (Exception)
            {
                throw new Exception(":SVR_CHECK_PROCESS_ERROR");
            }
        }

        /// <summary>
        /// Kills specified process if it is running
        /// </summary>
        /// <param name="processToKill">Name of the process to kill</param>
        /// <returns></returns>
        public static bool KillProcess(string processToKill)
        {
            if (processToKill.Length == 0)
            {
                throw new Exception(":SVR_RUN_INVALID_ARG");
            }

            Process[] processes = Process.GetProcesses();
            int occurrences = 0;
            foreach (var process in processes)
            {
                if (process.ProcessName.Contains(processToKill))
                {
                    occurrences++;
                    try
                    {
                        process.Kill();
                    }
                    catch(Exception)
                    {
                        // ignored
                    }
                }
            }

            return occurrences != 0;
        }

        /// <summary>
        /// Starts specified executable
        /// </summary>
        /// <param name="filename">Path to executable</param>
        /// <returns></returns>
        public static Process RunExecutable(string filename, string commandLineArgs = "")
        {
            FileInfo fileInfo = new FileInfo(filename);

            if(!fileInfo.Exists)
            {
                throw new Exception(":SVR_RUN_FILE_NOT_FOUND");
            }

            try
            {
                ProcessStartInfo info = new ProcessStartInfo(filename)
                {
                    UseShellExecute = false, 
                    CreateNoWindow = true, 
                    Arguments = commandLineArgs,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                return Process.Start(info);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Logouts all currently s
        /// </summary>
        public static void LogoutUsers()
        {
            using (ITerminalServer server = _manager.GetLocalServer())
            {
                server.Open();
                foreach(ITerminalServicesSession session in server.GetSessions())
                {
                    if (session.SessionId != 0)
                    {
                        session.Logoff();
                    }
                }
            }
        }

        #region RunAsWinApi
        [DllImport("wtsapi32.dll", SetLastError = true)]
        static extern bool WTSQueryUserToken(UInt32 sessionId, out IntPtr Token);

        /// <summary>
        /// The WTSGetActiveConsoleSessionId function retrieves the Remote Desktop Services session that
        /// is currently attached to the physical console. The physical console is the monitor, keyboard, and mouse.
        /// Note that it is not necessary that Remote Desktop Services be running for this function to succeed.
        /// </summary>
        /// <returns>The session identifier of the session that is attached to the physical console. If there is no
        /// session attached to the physical console, (for example, if the physical console session is in the process
        /// of being attached or detached), this function returns 0xFFFFFFFF.</returns>
        [DllImport("kernel32.dll")]
        private static extern uint WTSGetActiveConsoleSessionId();

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool CreateProcessAsUser(
            IntPtr hToken,
            string lpApplicationName,
            string lpCommandLine,
            ref SECURITY_ATTRIBUTES lpProcessAttributes,
            ref SECURITY_ATTRIBUTES lpThreadAttributes,
            bool bInheritHandles,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            ref STARTUPINFO lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInformation);

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        } 
        #endregion

        /// <summary>
        /// Runs specified executable as currently logged in user
        /// </summary>
        /// <param name="path"></param>
        /// <param name="commandLineArgs"></param>
        public static void RunExecutableAsUser(string path, string commandLineArgs)
        {
            IntPtr token;
            uint id = WTSGetActiveConsoleSessionId();
            WTSQueryUserToken(id, out token);
            PROCESS_INFORMATION proc_info = new PROCESS_INFORMATION();
            SECURITY_ATTRIBUTES procAttr = new SECURITY_ATTRIBUTES();
            SECURITY_ATTRIBUTES threadAttr = new SECURITY_ATTRIBUTES();
            STARTUPINFO startup_info = new STARTUPINFO();
            startup_info.cb = Marshal.SizeOf(startup_info);
            if(!CreateProcessAsUser(token, path, commandLineArgs, 
                ref procAttr, ref threadAttr, false, 0, IntPtr.Zero, String.Empty, ref startup_info, out proc_info))
            {
                throw new Exception(":SVR_RUN_FAILED");
            }
        }
    }
}
