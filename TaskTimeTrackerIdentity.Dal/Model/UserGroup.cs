using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskTimeTrackerIdentity.Dal.Model.Identity;
using Microsoft.AspNetCore.Identity;

namespace TaskTimeTrackerIdentity.Dal.Model
{
    [Table("UserGroup")]
    public class UserGroup
    {
        [Required, ForeignKey("UserRole")]
        public int RoleId { get; set; }
        [Required, ForeignKey("AppUser")]
        public int UserId { get; set; }
        [Required, ForeignKey("Group")]
        public int GroupId { get; set; }

        public Group Group { get; set; } = null!;
        public IdentityRole<int> Role { get; set; } = null!;
        public AppUser User { get; set; } = null!;
    }
}
