using System;
using TaskTimeTrackerIdentity.Dal.Model;
using Task = System.Threading.Tasks.Task;

namespace TaskTimeTrackerIdentity.Web.Services.Interfaces
{
    public interface ICommentService
    {
        public Task<List<Comment>> GetCommentsList();
        public Task<Comment> GetCommentById(int id);
        public Task<Comment> CreateComment(Comment comment);
        public Task UpdateComment(Comment comment);
        public Task DeleteComment(Comment comment);
    }
}

