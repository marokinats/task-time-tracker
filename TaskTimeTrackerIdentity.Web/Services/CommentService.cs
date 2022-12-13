using System;
using Microsoft.EntityFrameworkCore;
using TaskTimeTrackerIdentity.Dal.Context;
using TaskTimeTrackerIdentity.Dal.Model;
using TaskTimeTrackerIdentity.Web.Services.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace TaskTimeTrackerIdentity.Web.Services
{
    public class CommentService : ICommentService
    {
        public CommentService()
        {
        }

        private readonly ApplicationDbContext _context = null!;

        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetCommentsList()
        {
            return await _context.Comments
                                .Include(c => c.Author)
                                .ToListAsync();
        }

        public async Task<Comment> GetCommentById(int id)
        {
            Comment? comment = new();

            comment = await _context.Comments
                                    .Include(c => c.AuthorId)
                                    .FirstOrDefaultAsync(c => c.Id == id);
            return comment;
        }

        public async Task<Comment> CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task UpdateComment(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}

