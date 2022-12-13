using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using TaskTimeTrackerIdentity.Dal.Model;
using TaskTimeTrackerIdentity.Web.Services;
using TaskTimeTrackerIdentity.Web.Services.Interfaces;

using Task = System.Threading.Tasks.Task;
using MyBoard = TaskTimeTrackerIdentity.Dal.Model.Board;
using System.Xml;


namespace TaskTimeTrackerIdentity.Web.Shared
{
    public partial class NavLinkSpace
    {
        [Inject]
        protected ISpaceService? SpaceService { get; set; }

        [Inject]
        protected IBoardService? BoardService { get; set; }

        [Inject]
        protected AppState AppState { get; set; } = new();

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [CascadingParameter]
        public AuthenticationState? AuthenticationState { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState>? GetAuthenticationStateAsync { get; set; }

        private List<Space>? userSpaces;

        public List<MyBoard>? boards { get; set; }

        public ClaimsPrincipal CurrentUser { get; set; } = new();

        public int CurrentUserId { get; set; }


        protected override async Task OnInitializedAsync()
        {
            if (GetAuthenticationStateAsync != null)
            {
                AuthenticationState = await GetAuthenticationStateAsync;
                CurrentUser = AuthenticationState.User;
            }
            
            var CurrentUserIdString = CurrentUser.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;

            if (CurrentUserIdString != null)
            {
                CurrentUserId = Int32.Parse(CurrentUserIdString);
            }

            userSpaces = await SpaceService.GetUserSpacesByUserId(CurrentUserId);
            await InvokeAsync(() => {
                AppState.SetCurrentUserSpacesList(userSpaces);
                StateHasChanged();
            });
            

            //await InvokeAsync(StateHasChanged);
            //if (userSpaces == null && SpaceService != null)
            //{
            //    userSpaces = await SpaceService.GetUserSpacesByUserId(CurrentUserId);
            //    AppState.SetCurrentUserSpacesList(userSpaces);
            //}
        }

        public async Task SelectSpace(Space space)
        {
            await InvokeAsync(() => {
                AppState.SetCurrentSpace(space);
                StateHasChanged();
            });

            if (BoardService != null)
            {
                boards = await BoardService.GetUserBoardsBySpaceId(AppState.CurrentSpaceId, CurrentUserId);

                await InvokeAsync(() => {
                    AppState.SetCurrentBoardsList(boards);
                    StateHasChanged();
                });
                //await InvokeAsync(StateHasChanged);

                Board? board = boards.FirstOrDefault(b => b.Name == "Main Board");

                if (board != null)
                {
                    await InvokeAsync(() => {
                        AppState.SetCurrentBoard(board);
                        StateHasChanged();
                    });
                    //await InvokeAsync(StateHasChanged);
                }
                else
                {
                    AppState.CurrentBoardId = 0;
                }

                var link = "/space/" + AppState.CurrentSpaceId;
                if (AppState.CurrentBoardId != 0)
                {
                    link += "/" + AppState.CurrentBoardId;
                }
                NavigationManager?.NavigateTo(link);
            }
        }

        public async Task SelectBoard(Board board)
        {
            await InvokeAsync(() => {
                AppState.SetCurrentBoard(board);
                StateHasChanged();
            });

            var link = "/space/" + AppState.CurrentSpaceId + "/" + AppState.CurrentBoardId;
            NavigationManager?.NavigateTo(link);
        }

        public async Task SelectSpaceNavLink(string link, string spaseName)
        {
            Space space = new();
            space.Name = spaseName;
            await InvokeAsync(() => {
                AppState.SetCurrentSpace(space);
                StateHasChanged();
            });
            
            
            boards = new List<MyBoard>();
            await InvokeAsync(() => {
                AppState.SetCurrentBoardsList(boards);
                StateHasChanged();
            });
            
        }
    }
}

