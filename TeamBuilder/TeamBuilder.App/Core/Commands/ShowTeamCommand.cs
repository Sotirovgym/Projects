namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using TeamBuilder.App.Core.Commands.Constracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;

    public class ShowTeamCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            Check.CheckLength(1, args);

            var teamName = args[0];

            using (var context = new TeamBuilderDbContext())
            {
                var isTeamExist = CommandHelper.IsTeamExisting(teamName);
                if (!isTeamExist)
                {
                    throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
                }

                var team = context.Teams.FirstOrDefault(t => t.Name == teamName);

                var builder = new StringBuilder();

                builder.AppendLine($"[{team.Name}] [{team.Acronym}]");
                builder.AppendLine($"Members:");

                var teamMembers = context.UserTeams
                    .Where(ut => ut.TeamId == team.Id)
                    .ToArray();

                foreach (var teamMember in teamMembers)
                {
                    var member = context.Users.Find(teamMember.UserId);

                    builder.AppendLine($"--{member.Username}");
                }

                var result = builder.ToString().Trim();
                
                return result;
            }
        }
    }
}
