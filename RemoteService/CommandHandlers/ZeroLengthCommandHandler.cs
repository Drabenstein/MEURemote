namespace RemoteService.CommandHandlers
{
    class ZeroLengthCommandHandler : BaseCommandHandler
    {
        public override string Handle(string command)
        {
            if (command.Length == 0)
            {
                return ":SVR_CMD_ZERO_LENGTH";
            }

            return base.Handle(command);
        }
    }
}
