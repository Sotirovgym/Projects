using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamBuilder.Models
{
    public class Team
    {
        public Team()
        {
            EventTeams = new HashSet<TeamEvent>();
            Members = new HashSet<UserTeam>();
            Invitations = new HashSet<Invitation>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Description { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        public string Acronym { get; set; }

        public int CreatorId { get; set; }

        [Required]
        public User Creator { get; set; }

        public ICollection<TeamEvent> EventTeams { get; set; }
        public ICollection<UserTeam> Members { get; set; }
        public ICollection<Invitation> Invitations { get; set; }
    }
}
