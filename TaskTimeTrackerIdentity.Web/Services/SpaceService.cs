using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskTimeTrackerIdentity.Dal.Context;
using TaskTimeTrackerIdentity.Dal.Model;
using TaskTimeTrackerIdentity.Dal.Model.Identity;
using TaskTimeTrackerIdentity.Web.Services.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace TaskTimeTrackerIdentity.Web.Services
{
    public class SpaceService : ISpaceService
    {
        public SpaceService()
        {
        }

        private readonly ApplicationDbContext _context = null!;

        public SpaceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Space>> GetSpacesList()
        {
            return await _context.Spaces.ToListAsync();
        }

        public async Task<Space> GetSpaceById(int id)
        {
            Space? space = new();

            space = await _context.Spaces.FirstOrDefaultAsync(x => x.Id == id);

            return space;
        }

        public async Task<Space> GetSpaceByName(string name)
        {
            Space? space = new();

            space = await _context.Spaces.FirstOrDefaultAsync(x => x.Name == name);

            return space;
        }

        public async Task<Space> CreateSpace(Space space)
        {
            _context.Spaces.Add(space);
            await _context.SaveChangesAsync();
            return space;
        }

        public async Task UpdateSpace(Space space)
        {
            _context.Spaces.Update(space);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSpace(Space space)
        {
            _context.Spaces.Remove(space);
            await _context.SaveChangesAsync();
        }


        // UserSpace
        public async Task<UserSpace> CreateUserSpace(UserSpace userSpace)
        {
            _context.UserSpaces.Add(userSpace);
            await _context.SaveChangesAsync();
            return userSpace;
        }

        public async Task<List<Space>> GetUserSpacesByUserId(int id)
        {
            List<Space> returnList = new();

            var tempList = await _context.UserSpaces.Where(s => s.UserId == id).Include(s => s.Space).ToListAsync();


            returnList = tempList.Select(t => new Space()
            {
                Name = t.Space.Name,
                Id = t.Space.Id
                
            }).ToList();

            return returnList;
        }

        //User
        public async Task<AppUser> GetUserById(int id)
        {
            AppUser? user = new();

            user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<List<AppUser>> GetUsersBySpaceId(int spaseId)
        {
            List<AppUser> usersList = new();

            var tempList = await _context.UserSpaces.Where(us => us.SpaceId == spaseId).Include(s => s.User).ToListAsync();

            usersList = tempList.Select(t => new AppUser()
            {
                FirstName = t.User.FirstName,
                LastName = t.User.LastName,
                Id = t.User.Id
            }).ToList();

            return usersList;
        }

        // JoinSpaceRequest
        public async Task<JoinSpaceRequest> CreateJoinSpaceRequest(JoinSpaceRequest request)
        {
            _context.JoinSpaceRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }
    }
}

