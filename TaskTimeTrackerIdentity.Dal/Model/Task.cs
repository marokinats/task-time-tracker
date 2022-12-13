using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskTimeTrackerIdentity.Dal.Model.Identity;

namespace TaskTimeTrackerIdentity.Dal.Model
{
    [Table("Task")]
    public class Task
    {
        public Task()
        {
            Comments = new HashSet<Comment>();
            InverseParentTask = new HashSet<Task>();
        }

        [Key]
        public int Id { get; set; }
        [Required, ForeignKey("Board")]
        public int BoardId { get; set; }
        [Required, MaxLength(20)]
        public string TaskType { get; set; } = null!;
        [Required, MaxLength(100)]
        public string Title { get; set; } = null!;
        [MaxLength(500)]
        public string? Description { get; set; }
        [ForeignKey("AppUser")]
        public int? AssigneeId { get; set; }
        [ForeignKey("Task")]
        public int? ParentTaskId { get; set; }
        [MaxLength(20)]
        public string? Status { get; set; }
        
        public long? SpentTime { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public AppUser? Assignee { get; set; }
        public Board Board { get; set; } = null!;
        public Task? ParentTask { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Task> InverseParentTask { get; set; }
    }
}
