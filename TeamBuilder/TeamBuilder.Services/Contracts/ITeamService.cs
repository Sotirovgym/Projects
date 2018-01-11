using TeamBuilder.Models;

namespace TeamBuilder.Services.Contracts
{
    public interface ITeamService
    {
        Team Create(string name, string acronym, string description, int creatorId);

        void KickMember(string teamName, string username);

        void Disband(string teamName);

        void AddTeamTo(string eventName, string teamName);
    }
}
