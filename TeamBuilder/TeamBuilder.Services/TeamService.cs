using System.Linq;
using TeamBuilder.Data;
using TeamBuilder.Models;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.Services
{
    public class TeamService : ITeamService
    {
        private readonly TeamBuilderDbContext context;

        public TeamService(TeamBuilderDbContext context)
        {
            this.context = context;
        }

        public Team Create(string name, string acronym, string description, int creatorId)
        {
            var team = new Team()
            {
                Name = name,
                Acronym = acronym,
                Description = description,
                CreatorId = creatorId
            };

            context.Teams.Add(team);
            context.SaveChanges();

            return team;
        }

        public void KickMember(string teamName, string username)
        {
            var team = context.Teams.SingleOrDefault(t => t.Name == teamName);
            var user = context.Users.SingleOrDefault(u => u.Username == username);

            var userTeam = context.UserTeams.SingleOrDefault(ut => ut.TeamId == team.Id && ut.UserId == user.Id);

            user.UserTeams.Remove(userTeam);
            team.Members.Remove(userTeam);
            context.UserTeams.Remove(userTeam);

            context.SaveChanges();
        }

        public void Disband(string teamName)
        {
            var team = context.Teams.FirstOrDefault(t => t.Name == teamName);
            var user = team.Creator;

            var eventTeams = context.EventTeams
                .Where(ut => ut.TeamId == team.Id)
                .ToList();

            var userTeams = context.UserTeams
                .Where(ut => ut.TeamId == team.Id)
                .ToList();
            
            var invitations = context.Invitations
                .Where(ut => ut.TeamId == team.Id)
                .ToList();

            context.EventTeams.RemoveRange(eventTeams);
            context.UserTeams.RemoveRange(userTeams);
            context.Invitations.RemoveRange(invitations);
            context.Teams.Remove(team);
            context.SaveChanges();
        }

        public void AddTeamTo(string eventName, string teamName)
        {
            var team = context.Teams.SingleOrDefault(t => t.Name == teamName);
            var @event = context.Events
                .Where(e => e.Name == eventName)
                .OrderByDescending(e => e.StartDate)
                .First();

            var teamEvent = new TeamEvent()
            {
                Team = team,
                Event = @event
            };

            team.EventTeams.Add(teamEvent);
            @event.EventTeams.Add(teamEvent);
            context.EventTeams.Add(teamEvent);
            context.SaveChanges();
        }
    }
}
