using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using TaskTimeTrackerIdentity.Dal.Model;
namespace TaskTimeTrackerIdentity.Web.Shared
{
    public class AppState
    {
        // Current selected Space
        public int CurrentSpaceId { get; set; }
        public string CurrentSpaceName { get; set; } = "Connect with a Space";
        public List<Space>? CurrentUserSpacesList { get; set; }

        // Current selected Board
        public int CurrentBoardId { get; set; }
        public string CurrentBoardName { get; set; } = "Main Board";
        public List<Board>? CurrentBoardsList { get; set; }

        public EventCallback OnSpaceChange;

        public EventCallback OnBoardChange;

        public EventCallback OnBoardsListUpdate;

        public EventCallback OnUserSpacesListUpdate;

        public void SetCurrentSpace(Space space)
        {
            CurrentSpaceId = space.Id;
            CurrentSpaceName = space.Name;
            OnSpaceChange.InvokeAsync();
        }

        public void SetCurrentBoard(Board board)
        {
            CurrentBoardId = board.Id;
            CurrentBoardName = board.Name;
            OnBoardChange.InvokeAsync();
        }

        public void SetCurrentBoardsList(List<Board> boards)
        {
            CurrentBoardsList = boards;
            OnBoardsListUpdate.InvokeAsync();
        }

        public void SetCurrentUserSpacesList(List<Space> userSpaces)
        {
            CurrentUserSpacesList = userSpaces;
            OnUserSpacesListUpdate.InvokeAsync();
        }
    }
}

