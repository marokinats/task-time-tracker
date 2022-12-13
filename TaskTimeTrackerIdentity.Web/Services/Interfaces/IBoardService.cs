using System;
using TaskTimeTrackerIdentity.Dal.Model;
using Task = System.Threading.Tasks.Task;

namespace TaskTimeTrackerIdentity.Web.Services.Interfaces
{
    public interface IBoardService
    {
        public Task<Board> GetBoardById(int id);
        public Task<List<Board>> GetPublicBoardsBySpaceId(int id);
        public Task<List<Board>> GetUserBoardsBySpaceId(int spaceId, int userId);
        public Task<Board> CreateBoard(Board board);
        public Task UpdateBoard(Board board);
        public Task DeleteBoard(Board board);
    }
}

