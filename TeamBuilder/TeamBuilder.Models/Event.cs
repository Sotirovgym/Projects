using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamBuilder.Models
{
    public class Event
    {
        public Event()
        {
            EventTeams = new HashSet<TeamEvent>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Range(0, int.MaxValue)]
        public int CreatorId { get; set; }

        [Required]
        public User Creator { get; set; }

        public ICollection<TeamEvent> EventTeams { get; set; }
    }
}
