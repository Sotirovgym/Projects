namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Core.Commands.Constracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Models.Enums;
    using TeamBuilder.Services.Contracts;

    public class RegisterCommand : ICommand
    {
        private readonly IUserService userService;

        public RegisterCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] args)
        {
            Check.CheckLength(7, args);

            var username = args[0];
            if (Constants.MinUsernameLength > username.Length || Constants.MaxUsernameLength < username.Length)
            {
                throw new ArgumentException($"Username {username} not valid!");
            }

            var password = args[1];
            if (Constants.MinPasswordLength > password.Length || Constants.MaxPasswordLength < password.Length
                || !password.Any(char.IsDigit) || !password.Any(char.IsUpper))
            {
                throw new ArgumentException($"Password {password} not valid!");
            }

            var repeatPassword = args[2];
            if (password != repeatPassword)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.PasswordDoesNotMatch);
            }

            var firstName = args[3];
            var lastName = args[4];

            int age;
            var isNumber = int.TryParse(args[5], out age);
            if (age <= 0 || !isNumber)
            {
                throw new ArgumentException(Constants.ErrorMessages.AgeNotValid);
            }

            Gender gender;
            var isGenderValid = Enum.TryParse(args[6], out gender);
            if (!isGenderValid)
            {
                throw new ArgumentException(Constants.ErrorMessages.GenderNotValid);
            }

            userService.Create(username, password, firstName, lastName, age, gender);

            return $"User created successfully";
        }
    }
}
