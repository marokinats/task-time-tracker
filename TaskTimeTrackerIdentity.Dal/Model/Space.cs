using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTimeTrackerIdentity.Dal.Model
{
    [Table("Space")]
    public class Space
    {
        public Space()
        {
            UserSpaces = new HashSet<UserSpace>();
        }

        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;

        public ICollection<UserSpace> UserSpaces { get; set; }
        public ICollection<Board> Boards { get; set; }
    }
}
