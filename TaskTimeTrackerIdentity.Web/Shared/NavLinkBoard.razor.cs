using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using TaskTimeTrackerIdentity.Dal.Model;
using TaskTimeTrackerIdentity.Web.Services;
using TaskTimeTrackerIdentity.Web.Services.Interfaces;

using Task = System.Threading.Tasks.Task;
using MyBoard = TaskTimeTrackerIdentity.Dal.Model.Board;

namespace TaskTimeTrackerIdentity.Web.Shared
{
    public partial class NavLinkBoard
    {
        [Inject]
        protected IBoardService? BoardService { get; set; }

        [Inject]
        protected AppState AppState { get; set; } = new();

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        public List<MyBoard>? boards { get; set; }

        [CascadingParameter]
        public AuthenticationState? AuthenticationState { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState>? GetAuthenticationStateAsync { get; set; }

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

            // InvokeAsync is inherited, it syncs the call back to the render thread
            //await InvokeAsync(() =>
            //{
            //    AppState.OnBoardChange += StateHasChanged;
            //    AppState.OnBoardsListUpdate += StateHasChanged;
            //});

            //if (AppState.CurrentSpaceId != 0 && BoardService != null)
            //{
            //    boards = await BoardService.GetUserBoardsBySpaceId(AppState.CurrentSpaceId, CurrentUserId);
            //}

        }

        public void Dispose()
        {
            //AppState.OnBoardChange -= StateHasChanged;
            //AppState.OnBoardsListUpdate -= StateHasChanged;
        }

        public async Task SelectBoard(Board board)
        {
            AppState.SetCurrentBoard(board);
            //StateHasChanged();
            var link = "/space/" + AppState.CurrentSpaceId + "/" + AppState.CurrentBoardId;
            NavigationManager?.NavigateTo(link);
        }
    }
}

