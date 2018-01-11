namespace TeamBuilder.Services
{
    using System.Linq;
    using TeamBuilder.Data;
    using TeamBuilder.Models;
    using TeamBuilder.Models.Enums;
    using TeamBuilder.Services.Contracts;

    public class UserService : IUserService
    {
        private readonly TeamBuilderDbContext context;

        public UserService(TeamBuilderDbContext context)
        {
            this.context = context;
        }

        public User ById(int id)
        {
            using (var context = new TeamBuilderDbContext())
            {
                var user = context.Users.Find(id);

                return user;
            }
        }

        public User ByUsername(string username)
        {
            using (var context = new TeamBuilderDbContext())
            {
                var user = context.Users.Single(u => u.Username == username);

                return user;
            }
        }

        public User ByUsernameAndPassword(string username, string password)
        {
            using (var context = new TeamBuilderDbContext())
            {
                var user = context.Users
                    .Single(u => u.Username == username 
                            && u.Password == password);

                return user;
            }
        }

        public User Create(string username, string password, string firstName, string lastName, int age, Gender gender)
        {
            using (var context = new TeamBuilderDbContext())
            {
                var user = new User()
                {
                    Username = username,
                    Password = password,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age,
                    Gender = gender
                };

                context.Users.Add(user);
                context.SaveChanges();

                return user;
            }
        }

        public void Delete(int id)
        {
            using (var context = new TeamBuilderDbContext())
            {
                var user = context.Users.Find(id);

                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
    }
}
