using System.Collections.Generic;

namespace Remote
{
    public static class ServerCommands
    {
        #region Commands
        public const string SVR_SHUTDOWN = "SHUTDOWN";
        public const string SVR_RESTART = "RESTART";
        public const string SVR_LOGOFF = "LOGOFF";
        public const string SVR_LOGOUT = "LOGOUT";
        public const string SVR_CHECKPROCESS = "CHECKPROCESS";
        public const string SVR_KILLPROCESS = "KILLPROCESS";
        public const string SVR_RUN = "RUN"; 
        #endregion
        private static Dictionary<string, string> _commandsWithDes = new Dictionary<string, string>()
        {
            {SVR_SHUTDOWN, "Zamyka system" },
            {SVR_RESTART, "Ponownie uruchamia system" },
            {SVR_LOGOUT, "Wylogowywuje obecnie zalogowanego użytkownika" },
            {SVR_LOGOFF, "Wylogowywuje obecnie zalogowanego użytkownika" },
            {SVR_RUN, "Urachamia proces (exe) z podanej ścieżki (domyślnie szuka w katalogu plików odebranych przez klienta)" },
            {SVR_CHECKPROCESS, "Sprawdza czy dany proces jest uruchomiony na komputerze" },
            {SVR_KILLPROCESS, "Natychmiastowo zamyka wszystkie procesy o danej nazwie" }
        };
        private static Dictionary<string, string> _commandsWithSyntax = new Dictionary<string, string>()
        {
            {SVR_SHUTDOWN, "Shutdown" },
            {SVR_RESTART, "Restart" },
            {SVR_LOGOUT, "LogOut" },
            {SVR_LOGOFF, "LogOff" },
            {SVR_RUN, "Run C:/Windows/System32/cmd.exe" },
            {SVR_CHECKPROCESS, "CheckProcess chrome" },
            {SVR_KILLPROCESS, "KillProcess chrome" }
        };

        /// <summary>
        /// Zwraca listę poleceń
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCommandList()
        {
            List<string> cmds = new List<string>();
            foreach (var item in _commandsWithDes)
            {
                cmds.Add(item.Key);
            }
            return cmds;
        }

        /// <summary>
        /// Zwraca słownik o kluczu = nazwa polecenia i wartości = opis polecenia
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetCommandsWithDescription()
        {
            return _commandsWithDes;
        }

        /// <summary>
        /// Zwraca słownik o kluczu = nazwa polecenia i wartości = przykładowa składnia
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetCommandsWithSyntax()
        {
            return _commandsWithSyntax;
        }
    }
}
