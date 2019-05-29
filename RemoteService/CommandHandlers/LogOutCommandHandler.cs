namespace RemoteService.CommandHandlers
{
    class LogOutCommandHandler : BaseCommandHandler
    {
        public override string Handle(string command)
        {
            if (command.ToUpper().StartsWith(ServerCommands.SvrLogoff) ||
                command.ToUpper().StartsWith(ServerCommands.SvrLogout))
            {
                CommonActions.LogoutUsers();
                return ":SVR_LOGOUT_OK";
            }

            return base.Handle(command);
        }
    }
}
