namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Core.Commands.Constracts;

    public class ExitCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            Environment.Exit(0);

            return string.Empty;
        }
    }
}
