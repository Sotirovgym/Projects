using System;
using System.Linq;
using TeamBuilder.Data;
using TeamBuilder.Models;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.Services
{
    public class EventService : IEventService
    {
        private readonly TeamBuilderDbContext context;

        public EventService(TeamBuilderDbContext context)
        {
            this.context = context;
        }

        public Event Create(string name, string description, DateTime startDate, DateTime endDate, int creatorId)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == creatorId);

            var newEvent = new Event()
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                CreatorId = creatorId
            };

            user.CreatedEvents.Add(newEvent);
            context.Events.Add(newEvent);
            context.SaveChanges();

            return newEvent;
        }
    }
}
