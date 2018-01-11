namespace TeamBuilder.Services.Contracts
{
    public interface IInvitationService
    {
        void InviteToTeam(string teamName, string userName);

        void AcceptInvite(string teamName, string userName);

        void DeclineInvite(string teamName);
    }
}
