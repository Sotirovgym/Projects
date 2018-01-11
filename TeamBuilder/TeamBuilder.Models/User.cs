using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamBuilder.Models.Enums;

namespace TeamBuilder.Models
{
    public class User
    {
        public User()
        {
            UserTeams = new HashSet<UserTeam>();
            ReceivedInvitations = new HashSet<Invitation>();
            CreatedUserTeams = new HashSet<UserTeam>();
            CreatedEvents = new HashSet<Event>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Username { get; set; }

        [MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        // Required Uppercase and digit
        [RegularExpression(@"(?=.*\d)(?=.*[A-Z]).*")]
        public string Password { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Range(0, int.MaxValue)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<UserTeam> UserTeams { get; set; }
        public ICollection<Invitation> ReceivedInvitations { get; set; }
        public ICollection<UserTeam> CreatedUserTeams { get; set; }
        public ICollection<Event> CreatedEvents { get; set; }
    }
}
