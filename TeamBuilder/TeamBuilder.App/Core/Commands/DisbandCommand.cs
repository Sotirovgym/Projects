namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Core.Commands.Constracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Services.Contracts;

    class DisbandCommand : ICommand
    {
        private readonly ITeamService teamService;

        public DisbandCommand(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        public string Execute(params string[] args)
        {
            Check.CheckLength(1, args);

            var teamName = args[0];

            var isTeamExist = CommandHelper.IsTeamExisting(teamName);
            if (!isTeamExist)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            var isCreatorOfTeam = CommandHelper.IsUserCreatorOfTeam(teamName, Session.User);
            if (!isCreatorOfTeam)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            if (Session.User == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            teamService.Disband(teamName);

            return $"{teamName} has disbanded!";
        }
    }
}
