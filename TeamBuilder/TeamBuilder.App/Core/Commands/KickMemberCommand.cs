namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Core.Commands.Constracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Services.Contracts;

    class KickMemberCommand : ICommand
    {
        private readonly ITeamService teamService;

        public KickMemberCommand(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        public string Execute(params string[] args)
        {
            Check.CheckLength(2, args);

            var teamName = args[0];
            var username = args[1];

            using (var context = new TeamBuilderDbContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Username == username);

                var isTeamExist = CommandHelper.IsTeamExisting(teamName);
                if (!isTeamExist)
                {
                    throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
                }

                var isUserExist = CommandHelper.IsUserExisting(username);
                if (!isUserExist)
                {
                    throw new ArgumentException(string.Format(Constants.ErrorMessages.UserNotFound, username));
                }

                var isMemberOfTeam = CommandHelper.IsMemberOfTeam(teamName, username);
                if (!isMemberOfTeam)
                {
                    throw new ArgumentException(string.Format(Constants.ErrorMessages.NotPartOfTeam, username, teamName));
                }

                if (Session.User == null)
                {
                    throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
                }

                var isCreatorOfTeam = CommandHelper.IsUserCreatorOfTeam(teamName, Session.User);
                if (!isCreatorOfTeam)
                {
                    throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
                }

                var isUserToKickIsCreator = CommandHelper.IsUserCreatorOfTeam(teamName, user);
                if (isUserToKickIsCreator)
                {
                    throw new InvalidOperationException($"Command not allowed. Use DisbandTeam instead.");
                }

                teamService.KickMember(teamName, username);
            }

            return $"User {username} was kicked from {teamName}!";
        }
    }
}
