namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Core.Commands.Constracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;
    using TeamBuilder.Services.Contracts;

    public class CreateTeamCommand : ICommand
    {
        private readonly ITeamService teamService;

        public CreateTeamCommand(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        public string Execute(params string[] args)
        {
            Check.CheckLength(3, args);

            var name = args[0];
            var acronym = args[1];
            var description = args[2];

            if (CommandHelper.IsTeamExisting(name))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamExists, name));
            }

            if (acronym.Length != 3)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InvalidAcronym, acronym));
            }

            if (Session.User == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            var creator = Session.User;

            teamService.Create(name, acronym, description, creator.Id);

            return $"Team {name} successfully created!";
        }
    }
}
