namespace TeamBuilder.Services
{
    using System.Linq;
    using TeamBuilder.App;
    using TeamBuilder.Data;
    using TeamBuilder.Models;
    using TeamBuilder.Services.Contracts;

    public class InvitationService : IInvitationService
    {
        private readonly TeamBuilderDbContext context;

        public InvitationService(TeamBuilderDbContext context)
        {
            this.context = context;
        }

        public void InviteToTeam(string teamName, string userName)
        {
            var team = context.Teams.FirstOrDefault(t => t.Name == teamName);
            var user = context.Users.FirstOrDefault(u => u.Username == userName);

            var invitation = new Invitation()
            {
                InvitedUserId = user.Id,
                TeamId = team.Id
            };

            context.Invitations.Add(invitation);
            context.SaveChanges();
        }

        public void AcceptInvite(string teamName, string userName)
        {
            var user = context.Users.SingleOrDefault(u => u.Username == userName);
            var team = this.context.Teams.SingleOrDefault(t => t.Name == teamName);

            var userTeam = new UserTeam
            {
                User = user,
                Team = team
            };

            user.UserTeams.Add(userTeam);
            team.Members.Add(userTeam);
            context.UserTeams.Add(userTeam);

            var invitation = context
                .Invitations
                .SingleOrDefault(i => i.InvitedUserId == user.Id &&
                        i.TeamId == team.Id && i.IsActive == true);

            invitation.IsActive = false;

            context.SaveChanges();
        }

        public void DeclineInvite(string teamName)
        {
            var user = Session.User;
            var team = context.Teams.SingleOrDefault(t => t.Name == teamName);

            var invitation = context.Invitations
                .SingleOrDefault(i => i.Team.Id == team.Id && i.InvitedUserId == user.Id && i.IsActive);

            invitation.IsActive = false;

            context.SaveChanges();
        }
    }
}
