using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using TaskTimeTrackerIdentity.Dal.Context;
using TaskTimeTrackerIdentity.Dal.Model;
using TaskTimeTrackerIdentity.Web.Services.Interfaces;
using Group = TaskTimeTrackerIdentity.Dal.Model.Group;
using Task = System.Threading.Tasks.Task;

namespace TaskTimeTrackerIdentity.Web.Services
{
    public class GroupService : IGroupService
    {
        public GroupService()
        {
        }

        private readonly ApplicationDbContext _context = null!;

        public GroupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Group>> GetGroupsList()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<Group> GetGroupById(int id)
        {
            Group? group = new();

            group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == id);

            return group;
        }

        public async Task<Group> CreateGroup(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task UpdateGroup(Group group)
        {
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGroup(Group group)
        {
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
        }


        // UserSpace
        public async Task<UserGroup> CreateUserGroup(UserGroup userGroup)
        {
            _context.UserGroups.Add(userGroup);
            await _context.SaveChangesAsync();
            return userGroup;
        }

        public async Task<List<Group>> GetUserGroupsByUserId(int id)
        {
            List<Group> returnList = new();

            var tempList = await _context.UserGroups
                                            .Where(s => s.UserId == id)
                                            .Include(s => s.Group)
                                            .ThenInclude(g => g.Space)
                                            .ToListAsync();

            returnList = tempList.Select(t => new Group()
            {
                Name = t.Group.Name,
                Id = t.Group.Id

            }).ToList();
            return returnList;
        }
    }
}

