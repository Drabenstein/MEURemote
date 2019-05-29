namespace RemoteService.CommandHandlers
{
    class ShutdownCommandHandler : BaseCommandHandler
    {
        public override string Handle(string command)
        {
            if (command.ToUpper().StartsWith(ServerCommands.SvrShutdown))
            {
                CommonActions.Shutdown();
                return ":SVR_SHUTDOWN_OK";
            }

            return base.Handle(command);
        }
    }
}