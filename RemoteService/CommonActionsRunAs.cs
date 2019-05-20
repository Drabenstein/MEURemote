using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RemoteService
{
    public static class CommonActionsRunAs
    {
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

        public static void RunExecutable(string path, string commandLineArgs)
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
