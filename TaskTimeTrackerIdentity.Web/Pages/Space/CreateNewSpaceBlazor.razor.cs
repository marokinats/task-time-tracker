using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using TaskTimeTrackerIdentity.Dal.Model;
using TaskTimeTrackerIdentity.Dal.Model.Identity;
using TaskTimeTrackerIdentity.Web.Services.Interfaces;
using SystemTask = System.Threading.Tasks.Task;
using MySpace = TaskTimeTrackerIdentity.Dal.Model.Space;
using MyBoard = TaskTimeTrackerIdentity.Dal.Model.Board;

using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using TaskTimeTrackerIdentity.Web.Shared;

namespace TaskTimeTrackerIdentity.Web.Pages.Space
{
    public partial class CreateNewSpaceBlazor
    {
        [Inject]
        protected ISpaceService SpaceService { get; set; }

        [Inject]
        protected IBoardService BoardService { get; set; }

        [Inject]
        protected AppState AppState { get; set; } = new();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        public AuthenticationState AuthenticationState { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> GetAuthenticationStateAsync { get; set; }

        public MySpace? Space { get; set; }

        public ClaimsPrincipal CurrentUser { get; set; }

        public string? CurrentUserEmail { get; set; }

        public int CurrentUserId { get; set; }
        

        protected override async SystemTask OnInitializedAsync()
        {
            Space = new MySpace();

            AuthenticationState = await GetAuthenticationStateAsync;
            CurrentUser = AuthenticationState.User;
            CurrentUserEmail = CurrentUser?.Identity?.Name;
            CurrentUserId = Int32.Parse(CurrentUser.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value);
        }

        private async SystemTask SubmitSpace()
        {

            // CreateSpace
            await SpaceService.CreateSpace(Space);

            UserSpace userSpace = new UserSpace()
            {
                UserId = CurrentUserId,
                RoleId = 1,
                SpaceId = Space.Id
            };

            // CreateUserSpace
            await SpaceService.CreateUserSpace(userSpace);

            // CreateBoards
            var boards = Space.Boards?.ToList();

            if (boards == null)
            {
                MyBoard spaceBoard = new MyBoard()
                {
                    Name = "Main Board",
                    SpaceId = Space.Id,
                    BoardAdminId = CurrentUserId
                };
                MyBoard userBoard = new MyBoard()
                {
                    Name = "User Board",
                    SpaceId = Space.Id,
                    BoardAdminId = CurrentUserId,
                    AccessType = Dal.Enum.AccessType.Private
                };

                await BoardService.CreateBoard(spaceBoard);
                await BoardService.CreateBoard(userBoard);

                boards = Space.Boards?.ToList();
            }

            List<MySpace> userSpaces = await SpaceService.GetUserSpacesByUserId(CurrentUserId);
            AppState.SetCurrentUserSpacesList(userSpaces);

            NavigationManager.NavigateTo("/");
        }
    }
}

