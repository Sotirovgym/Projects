using System;
using TeamBuilder.Models;

namespace TeamBuilder.Services.Contracts
{
    public interface IEventService
    {
        Event Create(string name, string description, DateTime startDate, DateTime endDate, int creatorId);
    }
}
