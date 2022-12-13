using System;
using Microsoft.EntityFrameworkCore;
using TaskTimeTrackerIdentity.Dal.Context;
using TaskTimeTrackerIdentity.Dal.Model;
using TaskTimeTrackerIdentity.Dal.Enum;
using TaskTimeTrackerIdentity.Web.Services.Interfaces;
using Task = System.Threading.Tasks.Task;
using System.Linq;

namespace TaskTimeTrackerIdentity.Web.Services
{
    public class BoardService: IBoardService
    {
        public BoardService()
        {
        }

        private readonly ApplicationDbContext _context = null!;

        public BoardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Board> GetBoardById(int id)
        {
            Board? board = new Board();

            board = await _context.Boards.FirstOrDefaultAsync(x => x.Id == id);

            return board;
        }

        public async Task<List<Board>> GetPublicBoardsBySpaceId(int id)
        {
            List<Board> boards = new();
            boards = await _context.Boards.Where(b => b.SpaceId == id && b.AccessType == AccessType.Public).ToListAsync();

            return boards;
        }

        public async Task<List<Board>> GetUserBoardsBySpaceId(int spaceId, int userId)
        {
            List<Board> publicBoards = await GetPublicBoardsBySpaceId(spaceId);

            List<Board> userBoards = await _context.Boards
                                                    .Where(b => b.BoardAdminId == userId &&
                                                           b.SpaceId == spaceId &&
                                                           b.AccessType != AccessType.Public)
                                                    .ToListAsync();
            userBoards.AddRange(publicBoards);

            return userBoards;
        }

        public async Task<Board> CreateBoard(Board board)
        {
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
            return board;
        }

        public async Task UpdateBoard(Board board)
        {
            _context.Boards.Update(board);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBoard(Board board)
        {
            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
        }
    }
}

