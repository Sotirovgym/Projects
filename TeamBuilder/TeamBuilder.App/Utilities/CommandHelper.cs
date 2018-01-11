namespace TeamBuilder.App.Utilities
{
    using System.Linq;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class CommandHelper
    {
        public static bool IsTeamExisting(string teamName)
        {
            using (var context = new TeamBuilderDbContext())
            {
                var result = context.Teams.Any(t => t.Name == teamName);

                return result;
            }
        }

        public static bool IsUserExisting(string username)
        {
            using (var context = new TeamBuilderDbContext())
            {
                var result = context.Users.Any(u => u.Username == username);

                return result;
            }
        }

        public static bool IsInviteExisting(string teamName, User user)
        {
            using (var context = new TeamBuilderDbContext())
            {
                var result = context.Invitations
                    .Any(i => i.Team.Name == teamName && i.InvitedUser == user);

                return result;
            }
        }

        public static bool IsUserCreatorOfTeam(string teamName, User creator)
        {
            using (var context = new TeamBuilderDbContext())
            {
                var result = context.Teams.Any(t => t.Name == teamName && t.Creator == creator);

                return result;
            }
        }

        public static bool IsUserCreatorOfEvent(string eventName, User creator)
        {
            using (var context = new TeamBuilderDbContext())
            {
                var result = context.Events
                    .Any(e => e.Name == eventName && e.Creator == creator);

                return result;
            }
        }

        public static bool IsMemberOfTeam(string teamName, string username)
        {
            using (var context = new TeamBuilderDbContext())
            {
                var result = context.Teams
                    .Any(t => t.Name == teamName && t.Members
                        .Any(ut => ut.User.Username == username));

                return result;
            }
        }

        public static bool IsEventExisting(string eventName)
        {
            using (var context = new TeamBuilderDbContext())
            {
                var result = context.Events
                    .Any(e => e.Name == eventName);

                return result;
            }
        }

        public static bool isTeamAlreadyAdded(string teamName, string eventName)
        {
            using (var context = new TeamBuilderDbContext())
            {
                var @event = context.Events
                .Where(e => e.Name == eventName)
                .OrderByDescending(e => e.StartDate)
                .First();

                var result = context.EventTeams
                    .Any(et => et.EventId == @event.Id && et.Team.Name == teamName);

                return result;
            }
        }
    }
}
