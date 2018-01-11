namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Core.Commands.Constracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;
    using TeamBuilder.Services;
    using TeamBuilder.Services.Contracts;

    public class InviteToTeamCommand : ICommand
    {
        private readonly IInvitationService invitationService;

        public InviteToTeamCommand(IInvitationService invitationService)
        {
            this.invitationService = invitationService;
        }

        public string Execute(params string[] args)
        {
            Check.CheckLength(2, args);

            var teamName = args[0];
            var username = args[1];

            using (var context = new TeamBuilderDbContext())
            {
                var team = context.Teams.SingleOrDefault(t => t.Name == teamName);
                var user = context.Users.SingleOrDefault(u => u.Username == username);                

                if (team == null || user == null)
                {
                    throw new ArgumentException(Constants.ErrorMessages.TeamOrUserNotExist);
                }

                if (Session.User == null)
                {
                    throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
                }

                var isCreator = CommandHelper.IsUserCreatorOfTeam(teamName, user);
                var isAlreadyInTeam = CommandHelper.IsMemberOfTeam(teamName, username);

                if (isCreator)
                {
                    var userTeam = new UserTeam()
                    {
                        Team = team,
                        User = user
                    };

                    user.CreatedUserTeams.Add(userTeam);
                    team.Members.Add(userTeam);
                    context.UserTeams.Add(userTeam);
                    context.SaveChanges();

                    return $"Team {teamName} invited {username}!";
                }

                if (isCreator || isAlreadyInTeam)
                {
                    throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
                }

                var invitation = context.Invitations.Where(i => i.TeamId == team.Id && i.InvitedUserId == user.Id && i.IsActive == true);

                if (invitation.Any())
                {
                    throw new InvalidOperationException(Constants.ErrorMessages.InviteIsAlreadySent);
                }
            }

            invitationService.InviteToTeam(teamName, username);

            return $"Team {teamName} invited {username}!";
        }
    }
}
