using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.App.Core.Commands.Constracts;
using TeamBuilder.App.Utilities;
namespace TeamBuilder.App.Core.Commands
{
    using TeamBuilder.Data;
    using TeamBuilder.Services.Contracts;

    public class LoginCommand : ICommand
    {
        private readonly IUserService userService;

        public LoginCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] args)
        {
            Check.CheckLength(2, args);

            var username = args[0];
            var password = args[1];

            using (var context = new TeamBuilderDbContext())
            {
                var isUserExist = context.Users.Any(u => u.Username == username && u.Password == password && u.IsDeleted == false);                

                if (!isUserExist)
                {
                    throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
                }

                if (Session.User != null)
                {
                    throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
                }

                var user = context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);

                Session.User = user;
            }
           
            return $"User {username} successfully logged in!";
        }
    }
}
