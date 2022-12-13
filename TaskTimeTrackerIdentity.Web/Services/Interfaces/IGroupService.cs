using System;
using TaskTimeTrackerIdentity.Dal.Model;
using Task = System.Threading.Tasks.Task;

namespace TaskTimeTrackerIdentity.Web.Services.Interfaces
{
    public interface IGroupService
    {
        public Task<List<Group>> GetGroupsList();
        public Task<Group> GetGroupById(int id);
        public Task<Group> CreateGroup(Group group);
        public Task UpdateGroup(Group group);
        public Task DeleteGroup(Group group);

        //UserSpase
        public Task<UserGroup> CreateUserGroup(UserGroup userGroup);
        public Task<List<Group>> GetUserGroupsByUserId(int id);
    }
}

