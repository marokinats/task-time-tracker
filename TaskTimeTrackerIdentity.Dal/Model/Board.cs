using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskTimeTrackerIdentity.Dal.Enum;
using TaskTimeTrackerIdentity.Dal.Model.Identity;

namespace TaskTimeTrackerIdentity.Dal.Model
{
    [Table("Board")]
    public class Board
    {
        public Board()
        {
            Tasks = new HashSet<Task>();
        }

        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;
        [Required, ForeignKey("Space")]
        public int SpaceId { get; set; }
        [Required, MaxLength(20)]
        public AccessType AccessType { get; set; } = AccessType.Public;
        [Required, ForeignKey("AppUser")]
        public int BoardAdminId { get; set; }

        public AppUser BoardAdmin { get; set; } = null!;
        public Space Space { get; set; } = null!;
        public ICollection<Task> Tasks { get; set; }
    }
}
