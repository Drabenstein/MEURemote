namespace RemoteService.CommandHandlers
{
    abstract class BaseCommandHandler : ICommandHandler
    {
        protected ICommandHandler _nextHandler;

        public ICommandHandler SetNextHandler(ICommandHandler handler)
        {
            _nextHandler = handler;

            return _nextHandler;
        }

        public virtual string Handle(string command)
        {
            return _nextHandler?.Handle(command);
        }
    }
}
