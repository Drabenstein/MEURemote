using System;

namespace RemoteService.CommandHandlers
{
    class CheckProcessCommandHandler : BaseCommandHandler
    {
        public override string Handle(string command)
        {
            if (command.ToUpper().StartsWith(ServerCommands.SvrCheckProcess))
            {
                string procName = command.Substring(ServerCommands.SvrCheckProcess.Length + 1);
                try
                {
                    bool isRunning = CommonActions.CheckProcessRunning(procName);
                    if (isRunning)
                    {
                        return ":SVR_CHECK_PROCESS_GOOD";
                    }
                    else
                    {
                        return ":SVR_CHECK_PROCESS_BAD";
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
