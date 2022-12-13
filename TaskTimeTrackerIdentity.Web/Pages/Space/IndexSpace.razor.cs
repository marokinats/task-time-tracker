using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using TaskTimeTrackerIdentity.Dal.Model;
using TaskTimeTrackerIdentity.Web.Services.Interfaces;
using TaskTimeTrackerIdentity.Web.Shared;

using SystemTask = System.Threading.Tasks.Task;
using MyTask = TaskTimeTrackerIdentity.Dal.Model.Task;
using System.Threading.Tasks;
using TaskTimeTrackerIdentity.Dal.Model.Identity;

namespace TaskTimeTrackerIdentity.Web.Pages.Space
{
    public partial class IndexSpace
    {
        [Inject]
        protected IMyTaskService MyTaskService { get; set; }

        [Inject]
        protected ISpaceService SpaceService { get; set; }

        [Inject]
        protected ICommentService CommentService { get; set; }

        [Inject]
        protected AppState AppState { get; set; } = new();

        [Parameter]
        public int SpaceId { get; set; }

        [Parameter]
        public string SelectedBoard { get; set; } = "";

        private MyTask Task { get; set; }

        private List<MyTask> MyTasks { get; set; }

        private List<MyTask> MyTasksNoParent { get; set; }

        public List<MyTask> RelatedTasks { get; set; }

        public List<AppUser> UsersByCurrentSpace { get; set; }

        private ConfirmationModal myConfirmationModal;

        private ModalForm? modalForm;

        private ModalFormType formType;

        private string formTitle = "";

        private string taskType = "";

        

        protected override async SystemTask OnInitializedAsync()
        {
            Task = new MyTask();
            MyTasks = await MyTaskService.GetTasksListByBoardId(AppState.CurrentBoardId);
            //MyTasksNoParent = MyTasks.Where(t => t.ParentTaskId == null && t.TaskType == "Task").ToList();

            RelatedTasks = new();
        }

        protected override async SystemTask OnParametersSetAsync()
        {
            MyTasks = await MyTaskService.GetTasksListByBoardId(AppState.CurrentBoardId);
            MyTasksNoParent = MyTasks.Where(t => t.ParentTaskId == null && t.TaskType == "Task").ToList();
            UsersByCurrentSpace = await SpaceService.GetUsersBySpaceId(AppState.CurrentSpaceId);
        }

        //ModalForm
        private async SystemTask DeleteConfirmedClick()
        {
            await MyTaskService.DeleteTask(Task);
            MyTasks.RemoveAll(x => x.Id == Task.Id);
        }

        private async SystemTask ConfirmAddTask()
        {
            Task.BoardId = AppState.CurrentBoardId;
            await MyTaskService.CreateTask(Task);
            MyTasks = await MyTaskService.GetTasksListByBoardId(AppState.CurrentBoardId);
            StateHasChanged();
            await modalForm.Hide();
        }

        private async SystemTask ConfirmSaveTask()
        {
            await MyTaskService.UpdateTask(Task);
            foreach(MyTask relatedTask in RelatedTasks)
            {
                await MyTaskService.UpdateTask(relatedTask);
            }
            MyTasks = await MyTaskService.GetTasksListByBoardId(AppState.CurrentBoardId);
            StateHasChanged();
            RelatedTasks = new();
            await modalForm.Hide();
        }

        private async SystemTask CancelTaskClick()
        {
            RelatedTasks = new();
            await modalForm.Hide();
        }

        // Board buttons actions
        private async SystemTask CreateNewTaskClick(string type)
        {
            formTitle = "Create";
            formType = ModalFormType.Add;
            taskType = type;

            Task = new MyTask();
            Task.TaskType = type;
            await modalForm.Show();
        }

        private async SystemTask EditTaskClick(int taskId)
        {
            formTitle = "View & Edit";
            formType = ModalFormType.Edit;
            Task = await MyTaskService.GetTaskById(taskId);
            taskType = Task.TaskType;

            await modalForm.Show();
        }

        // Select handlers
        public async SystemTask OnSelectRelatedTasks(ChangeEventArgs e)
        {
            
            int taskId = Int32.Parse(e.Value.ToString());
            if (taskId > 0)
            {
                MyTask relatedTask = await MyTaskService.GetTaskById(taskId);
                relatedTask.ParentTaskId = Task.Id;
                RelatedTasks.Add(relatedTask);
                MyTasksNoParent.Remove(relatedTask);
                StateHasChanged();
            }
        }

        public async SystemTask OnSelectTaskAssignee(ChangeEventArgs e)
        {

        }
    }
}
