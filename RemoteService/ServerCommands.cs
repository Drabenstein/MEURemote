﻿using System.Collections.Generic;

namespace RemoteService
{
    public static class ServerCommands
    {
        public const string SvrShutdown = "SHUTDOWN";
        public const string SvrRestart = "RESTART";
        public const string SvrLogoff = "LOGOFF";
        public const string SvrLogout = "LOGOUT";
        public const string SvrCheckProcess = "CHECKPROCESS";
        public const string SvrKillProcess = "KILLPROCESS";
        public const string SvrRun = "RUN";

        /// <summary>
        /// Gets commands list
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCommandsList()
        {
            List<string> commands = new List<string>();
            commands.Add(SvrShutdown);
            commands.Add(SvrRun);
            commands.Add(SvrRestart);
            commands.Add(SvrLogout);
            commands.Add(SvrLogoff);
            commands.Add(SvrKillProcess);
            commands.Add(SvrCheckProcess);
            return commands;
        }
    }
}
