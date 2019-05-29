using System;
using System.IO;
using System.Linq;

namespace RemoteService.CommandHandlers
{
    class RunCommandHandler : BaseCommandHandler
    {
        private readonly string _workingDir;

        public RunCommandHandler(string workingDir)
        {
            _workingDir = workingDir ?? throw new ArgumentNullException(nameof(workingDir));
        }

        public override string Handle(string command)
        {
            if (command.ToUpper().StartsWith(ServerCommands.SvrRun))
            {
                string[] parameters = command.Substring(3).Split(' ');
                string path = parameters[0];
                string args = parameters.Aggregate((acc, arg) => $"{acc} {arg}");

                bool isReceivedFilesFolder = false;
                DirectoryInfo directoryInfo = new DirectoryInfo(_workingDir);

                foreach (var item in directoryInfo.GetFiles())
                {
                    if (item.Name.Equals(path))
                    {
                        isReceivedFilesFolder = true;
                        break;
                    }
                }

                path = isReceivedFilesFolder ? Path.Combine(_workingDir, path) : path;

                try
                {
                    CommonActions.RunExecutable(path, args);
                    return ":SVR_RUN_OK";
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
