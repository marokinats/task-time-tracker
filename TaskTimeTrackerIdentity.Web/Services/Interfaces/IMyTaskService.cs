using System;
using System.Numerics;
using MyTask = TaskTimeTrackerIdentity.Dal.Model.Task;

namespace TaskTimeTrackerIdentity.Web.Services.Interfaces
{
    public interface IMyTaskService
    {
        public Task<List<MyTask>> GetTasksListByBoardId(int boardId);
        public Task<MyTask> GetTaskById(int taskId);
        public Task<MyTask> CreateTask(MyTask myTask);
        public Task UpdateTask(MyTask myTask);
        public Task DeleteTask(MyTask myTask);
    }
}

