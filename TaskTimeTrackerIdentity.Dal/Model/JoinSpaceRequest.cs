using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskTimeTrackerIdentity.Dal.Model.Identity;

namespace TaskTimeTrackerIdentity.Dal.Model
{
    [Table("JoinSpaceRequest")]
    public class JoinSpaceRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool Approved { get; set; } = false!;
        [Required, ForeignKey("AppUser")]
        public int UserId { get; set; }
        [Required, ForeignKey("Space")]
        public int SpaceId { get; set; }

        public Space Space { get; set; } = null!;
        public AppUser User { get; set; } = null!;
    }
}

