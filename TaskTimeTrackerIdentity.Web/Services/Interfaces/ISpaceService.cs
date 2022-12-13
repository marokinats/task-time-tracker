using System;
using TaskTimeTrackerIdentity.Dal.Model;
using TaskTimeTrackerIdentity.Dal.Model.Identity;
using Task = System.Threading.Tasks.Task;

namespace TaskTimeTrackerIdentity.Web.Services.Interfaces
{
    public interface ISpaceService
    {
        public Task<List<Space>> GetSpacesList();
        public Task<Space> GetSpaceById(int id);
        public Task<Space> GetSpaceByName(string name);
        public Task<Space> CreateSpace(Space space);
        public Task UpdateSpace(Space space);
        public Task DeleteSpace(Space space);

        //UserSpase
        public Task<UserSpace> CreateUserSpace(UserSpace userSpace);
        public Task<List<Space>> GetUserSpacesByUserId(int id);

        //User
        public Task<AppUser> GetUserById(int id);
        public Task<List<AppUser>> GetUsersBySpaceId(int spaseId);

        // JoinSpaceRequest
        public Task<JoinSpaceRequest> CreateJoinSpaceRequest(JoinSpaceRequest request);
    }
}

