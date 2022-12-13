using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskTimeTrackerIdentity.Dal.Model.Identity;

namespace TaskTimeTrackerIdentity.Dal.Model
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(1000)]
        public string Text { get; set; } = null!;
        [Required, ForeignKey("AppUser")]
        public int AuthorId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required, ForeignKey("Task")]
        public int TaskId { get; set; }

        public AppUser Author { get; set; } = null!;
        public Task Task { get; set; } = null!;
    }
}
