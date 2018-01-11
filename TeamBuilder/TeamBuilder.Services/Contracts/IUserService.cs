using TeamBuilder.Models;
using TeamBuilder.Models.Enums;

namespace TeamBuilder.Services.Contracts
{
    public interface IUserService
    {
        User ById(int id);

        User ByUsername(string Username);

        User ByUsernameAndPassword(string username, string password);

        User Create(string username, string password, string firstName, string lastName, int age, Gender gender);

        void Delete(int id);
    }
}
