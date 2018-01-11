namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Core.Commands.Constracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Services.Contracts;

    public class AcceptInviteCommand : ICommand
    {
        private readonly IInvitationService invitationService;

        public AcceptInviteCommand(IInvitationService invitationService)
        {
            this.invitationService = invitationService;
        }

        public string Execute(params string[] args)
        {
            Check.CheckLength(1, args);

            var teamName = args[0];

            var user = Session.User;

            if (Session.User == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            var isTeamExist = CommandHelper.IsTeamExisting(teamName);

            if (!isTeamExist)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            var isInviteExist = CommandHelper.IsInviteExisting(teamName, user);

            if (!isInviteExist)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InviteNotFound, teamName));
            }

            invitationService.AcceptInvite(teamName, user.Username);

            return $"User {user.Username} joined team {teamName}!";
        }
    }
}
