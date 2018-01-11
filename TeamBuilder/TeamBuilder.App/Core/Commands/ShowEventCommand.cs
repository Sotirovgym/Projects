namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using TeamBuilder.App.Core.Commands.Constracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;

    public class ShowEventCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            Check.CheckLength(1, args);

            var eventName = args[0];

            using (var context = new TeamBuilderDbContext())
            {
                var isEventExist = CommandHelper.IsEventExisting(eventName);
                if (!isEventExist)
                {
                    throw new ArgumentException(string.Format(Constants.ErrorMessages.EventNotFound, eventName));
                }

                var @event = context.Events
                .Where(e => e.Name == eventName)
                .OrderByDescending(e => e.StartDate)
                .First();

                var builder = new StringBuilder();

                builder.AppendLine($"[{eventName}] [{@event.StartDate}] [{@event.EndDate}] [{@event.Description}]");
                builder.AppendLine("Teams:");

                var eventTeams = context.EventTeams
                    .Where(et => et.EventId == @event.Id)
                    .ToList();

                foreach (var eventTeam in eventTeams)
                {
                    var team = context.Teams.Find(eventTeam.TeamId);
                    builder.AppendLine($"-{team.Name}");
                }

                var result = builder.ToString().Trim();

                return result;
            }
        }
    }
}
