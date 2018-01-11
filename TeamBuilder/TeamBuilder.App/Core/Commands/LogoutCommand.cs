namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Core.Commands.Constracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Services.Contracts;

    public class LogoutCommand : ICommand
    {
        private readonly IUserService userService;

        public LogoutCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] args)
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            var user = Session.User;

            Session.User = null;

            return $"User {user.Username} successfully logged out!";
        }
    }
}
