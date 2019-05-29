using System.Collections.Generic;

namespace Remote
{
    public static class ServerCommands
    {
        #region Commands
        public const string SvrShutdown = "SHUTDOWN";
        public const string SvrRestart = "RESTART";
        public const string SvrLogoff = "LOGOFF";
        public const string SvrLogout = "LOGOUT";
        public const string SvrCheckprocess = "CHECKPROCESS";
        public const string SvrKillprocess = "KILLPROCESS";
        public const string SvrRun = "RUN";
        #endregion
        private static Dictionary<string, string> _commandsWithDes = new Dictionary<string, string>()
        {
            [SvrShutdown] = "Zamyka system",
            [SvrRestart] = "Ponownie uruchamia system",
            [SvrLogout] = "Wylogowywuje obecnie zalogowanego użytkownika",
            [SvrLogoff] = "Wylogowywuje obecnie zalogowanego użytkownika",
            [SvrRun] = "Urachamia proces (exe) z podanej ścieżki (domyślnie szuka w katalogu plików odebranych przez klienta)",
            [SvrCheckprocess] = "Sprawdza czy dany proces jest uruchomiony na komputerze",
            [SvrKillprocess] = "Natychmiastowo zamyka wszystkie procesy o danej nazwie"
        };
        private static Dictionary<string, string> _commandsSyntaxExamples = new Dictionary<string, string>()
        {
            [SvrShutdown] = "Shutdown",
            [SvrRestart] = "Restart",
            [SvrLogout] = "LogOut",
            [SvrLogoff] = "LogOff",
            [SvrRun] = "Run C:/Windows/System32/cmd.exe",
            [SvrCheckprocess] = "CheckProcess chrome",
            [SvrKillprocess] = "KillProcess chrome"
        };

        /// <summary>
        /// Gets list of available commands
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCommandsList()
        {
            List<string> commands = new List<string>();
            foreach (var command in _commandsWithDes)
            {
                commands.Add(command.Key);
            }
            return commands;
        }

        /// <summary>
        /// Gets commands' descriptions
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetCommandsDescriptions()
        {
            return _commandsWithDes;
        }

        /// <summary>
        /// Gets commands' syntax examples
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetCommandsSyntaxExamples()
        {
            return _commandsSyntaxExamples;
        }
    }
}
