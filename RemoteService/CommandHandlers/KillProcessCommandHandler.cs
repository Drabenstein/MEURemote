using System;

namespace RemoteService.CommandHandlers
{
    class KillProcessCommandHandler : BaseCommandHandler
    {
        public override string Handle(string command)
        {
            if (command.ToUpper().StartsWith(ServerCommands.SvrKillProcess))
            {
                string procName = command.Substring(ServerCommands.SvrKillProcess.Length + 1).Trim();
                try
                {
                    bool isKilled = CommonActions.KillProcess(procName);
                    if (isKilled)
                    {
                        return ":SVR_KILL_PROCESS_OK";
                    }
                    else
                    {
                        return ":SVR_KILL_PROCESS_FAIL";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

            return base.Handle(command);
        }
    }
}
