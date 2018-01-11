using System.ComponentModel.DataAnnotations;

namespace TeamBuilder.Models
{
    public class Invitation
    {
        public Invitation()
        {
            this.IsActive = true;
        }

        public int Id { get; set; }

        public int InvitedUserId { get; set; }

        [Required]
        public User InvitedUser { get; set; }

        public int TeamId { get; set; }

        [Required]
        public Team Team { get; set; }

        public bool IsActive { get; set; }
    }
}
