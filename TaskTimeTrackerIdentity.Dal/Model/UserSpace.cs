using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskTimeTrackerIdentity.Dal.Model.Identity;
using Microsoft.AspNetCore.Identity;

namespace TaskTimeTrackerIdentity.Dal.Model
{
    [Table("UserSpace")]
    public class UserSpace
    {
        [Required, ForeignKey("IdentityRole")]
        public int RoleId { get; set; }
        [Required, ForeignKey("AppUser")]
        public int UserId { get; set; }
        [Required, ForeignKey("Space")]
        public int SpaceId { get; set; }

        public IdentityRole<int> Role { get; set; } = null!;
        public Space Space { get; set; } = null!;
        public AppUser User { get; set; } = null!;
    }
}
