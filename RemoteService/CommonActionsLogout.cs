using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassia;

namespace RemoteService
{
    public static class CommonActionsLogout
    {
        private static readonly ITerminalServicesManager _manager = new TerminalServicesManager();

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
    }
}
