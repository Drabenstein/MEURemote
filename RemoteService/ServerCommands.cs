using System.Collections.Generic;

namespace RemoteService
{
    public static class ServerCommands
    {
        public const string SVR_SHUTDOWN = "SHUTDOWN";
        public const string SVR_RESTART = "RESTART";
        public const string SVR_LOGOFF = "LOGOFF";
        public const string SVR_LOGOUT = "LOGOUT";
        public const string SVR_CHECKPROCESS = "CHECKPROCESS";
        public const string SVR_KILLPROCESS = "KILLPROCESS";
        public const string SVR_RUN = "RUN";

        /// <summary>
        /// Zwraca listę poleceń
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCommandList()
        {
            List<string> commands = new List<string>();
            commands.Add(SVR_SHUTDOWN);
            commands.Add(SVR_RUN);
            commands.Add(SVR_RESTART);
            commands.Add(SVR_LOGOUT);
            commands.Add(SVR_LOGOFF);
            commands.Add(SVR_KILLPROCESS);
            commands.Add(SVR_CHECKPROCESS);
            return commands;
        }
    }
}
