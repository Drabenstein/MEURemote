namespace RemoteService
{
    interface ICommandHandler
    {
        string Handle(string command);
        ICommandHandler SetNextHandler(ICommandHandler handler);
    }
}
