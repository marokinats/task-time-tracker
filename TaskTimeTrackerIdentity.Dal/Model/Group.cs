using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTimeTrackerIdentity.Dal.Model
{
    [Table("Group")]
    public class Group
    {
        public Group()
        {
            Boards = new HashSet<Board>();
            UserGroups = new HashSet<UserGroup>();
        }

        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;
        [Required, ForeignKey("Space")]
        public int SpaceId { get; set; }

        public Space Space { get; set; } = null!;
        public ICollection<Board> Boards { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
    }
}
