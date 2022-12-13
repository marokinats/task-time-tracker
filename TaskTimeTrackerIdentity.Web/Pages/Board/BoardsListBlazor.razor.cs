using System;
using System.Numerics;
using Microsoft.AspNetCore.Components;
using TaskTimeTrackerIdentity.Web.Services;
using TaskTimeTrackerIdentity.Web.Services.Interfaces;
using MyBoard = TaskTimeTrackerIdentity.Dal.Model.Board;
using SystemTask = System.Threading.Tasks.Task;

namespace TaskTimeTrackerIdentity.Web.Pages.Board
{
    public partial class BoardsListBlazor
    {
        [Inject]

        protected IBoardService BoardService { get; set; }
        private List<MyBoard> Boards { get; set; }

        protected override async SystemTask OnInitializedAsync()
        {
            
        }

    }
}
