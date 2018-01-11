namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Globalization;
    using TeamBuilder.App.Core.Commands.Constracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;
    using TeamBuilder.Services.Contracts;

    public class CreateEventCommand : ICommand
    {
        private readonly IEventService eventService;

        public CreateEventCommand(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public string Execute(params string[] args)
        {
            Check.CheckLength(6, args);

            var name = args[0];
            var description = args[1];

            DateTime startDate;
            DateTime endDate;

            if (!DateTime.TryParseExact($"{args[2]} {args[3]}", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidDateFormat);
            }

            if (!DateTime.TryParseExact($"{args[4]} {args[5]}", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidDateFormat);
            }

            if (startDate > endDate)
            {
                throw new ArgumentException("Start date should be before end date.");
            }

            if (Session.User == null)
            {
                throw new InvalidCastException(Constants.ErrorMessages.LoginFirst);
            }

            var creator = Session.User;

            eventService.Create(name, description, startDate, endDate, creator.Id);
            
            return $"Event {name} was created successfully!";
        }
    }
}
