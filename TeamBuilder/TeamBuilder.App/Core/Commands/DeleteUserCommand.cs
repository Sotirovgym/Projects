namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Core.Commands.Constracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Services.Contracts;

    public class DeleteUserCommand : ICommand
    {
        private readonly IUserService userService;

        public DeleteUserCommand(IUserService userService)
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

            userService.Delete(user.Id);

            Session.User = null;

            return $"User {user.Username} was deleted successfully!";
        }
    }
}
