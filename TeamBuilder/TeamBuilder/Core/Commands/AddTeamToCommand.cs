namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Core.Commands.Constracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Services.Contracts;

    public class AddTeamToCommand : ICommand
    {
        private readonly ITeamService teamService;

        public AddTeamToCommand(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        public string Execute(params string[] args)
        {
            Check.CheckLength(2, args);

            var eventName = args[0];
            var teamName = args[1];

            var isEventExist = CommandHelper.IsEventExisting(eventName);
            if (!isEventExist)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.EventNotFound, eventName));
            }

            var isTeamExist = CommandHelper.IsTeamExisting(teamName);
            if (!isTeamExist)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            if (Session.User == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            var isCreatorOfEvent = CommandHelper.IsUserCreatorOfEvent(eventName, Session.User);
            if (!isCreatorOfEvent)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            var isTeamAlreadyAdded = CommandHelper.isTeamAlreadyAdded(teamName, eventName);
            if (isTeamAlreadyAdded)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CannotAddSameTeamTwice);
            }

            teamService.AddTeamTo(eventName, teamName);

            return $"Team {teamName} added for {eventName}!";
        }
    }
}
