using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;

namespace TaskTimeTrackerIdentity.Dal.Model.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser()
        {
            Boards = new HashSet<Board>();
            Comments = new HashSet<Comment>();
            Tasks = new HashSet<Task>();
            UserGroups = new HashSet<UserGroup>();
            UserSpaces = new HashSet<UserSpace>();
        }

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = null!;

        public ICollection<Board> Boards { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
        public ICollection<UserSpace> UserSpaces { get; set; }
    }
}

