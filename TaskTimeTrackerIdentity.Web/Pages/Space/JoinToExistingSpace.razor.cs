using System;
using System.Security.Claims;
using System.Xml;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using TaskTimeTrackerIdentity.Dal.Model;
using TaskTimeTrackerIdentity.Web.Services;
using TaskTimeTrackerIdentity.Web.Services.Interfaces;

using SystemTask = System.Threading.Tasks.Task;
using MySpace = TaskTimeTrackerIdentity.Dal.Model.Space;
using TaskTimeTrackerIdentity.Web.Shared;

namespace TaskTimeTrackerIdentity.Web.Pages.Space
{
    public partial class JoinToExistingSpace
    {
        [Inject]
        protected ISpaceService SpaceService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        protected AppState AppState { get; set; } = new();

        [CascadingParameter]
        public AuthenticationState? AuthenticationState { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> GetAuthenticationStateAsync { get; set; }

        public JoinSpaceRequest? JoinSpaceRequest { get; set; }

        public string JoinSpaceName { get; set; } = "";

        public ClaimsPrincipal CurrentUser { get; set; }

        public int CurrentUserId { get; set; }


        protected override async SystemTask OnInitializedAsync()
        {
            JoinSpaceRequest = new JoinSpaceRequest();

            AuthenticationState = await GetAuthenticationStateAsync;
            CurrentUser = AuthenticationState.User;
            CurrentUserId = Int32.Parse(CurrentUser.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value);
        }

        private async SystemTask SubmitJoinSpaceRequest()
        {
            MySpace spaceToJoin = new();

            if (SpaceService != null && JoinSpaceName != null && JoinSpaceName.Length > 0)
            {
                spaceToJoin = await SpaceService.GetSpaceByName(JoinSpaceName);
            }

            if (SpaceService != null && JoinSpaceRequest != null && spaceToJoin?.Id != null && CurrentUserId > 0)
            {
                JoinSpaceRequest.SpaceId = spaceToJoin.Id;
                JoinSpaceRequest.UserId = CurrentUserId;

                await SpaceService.CreateJoinSpaceRequest(JoinSpaceRequest);

                NavigationManager.NavigateTo("/");
            }
            
        }
    }
}

