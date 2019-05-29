namespace RemoteService.CommandHandlers
{
    class RestartCommandHandler : BaseCommandHandler
    {
        public override string Handle(string command)
        {
            if (command.ToUpper().StartsWith(ServerCommands.SvrRestart))
            {
                CommonActions.Shutdown("/r /f /t 0");
                return ":SVR_RESTART_OK";
            }

            return base.Handle(command);
        }
    }
}
