namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Core.Commands.Constracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Services.Contracts;

    public class DeclineInviteCommand : ICommand
    {
        private readonly IInvitationService invitationService;
        
        public DeclineInviteCommand(IInvitationService invitationService)
        {
            this.invitationService = invitationService;
        }

        public string Execute(params string[] args)
        {
            Check.CheckLength(1, args);

            var teamName = args[0];

            if (Session.User == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            var isTeamExist = CommandHelper.IsTeamExisting(teamName);

            if (!isTeamExist)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            var isInviteExist = CommandHelper.IsInviteExisting(teamName, Session.User);
            if (!isInviteExist)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InviteNotFound, teamName));
            }

            invitationService.DeclineInvite(teamName);

            return $"Invite from {teamName} declined.";
        }
    }
}
