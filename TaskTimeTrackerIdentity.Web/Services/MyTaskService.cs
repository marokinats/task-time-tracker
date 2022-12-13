using System;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
using TaskTimeTrackerIdentity.Dal.Context;
using TaskTimeTrackerIdentity.Dal.Model;
using TaskTimeTrackerIdentity.Web.Services.Interfaces;
using MyTask = TaskTimeTrackerIdentity.Dal.Model.Task;
using ServiceTask = System.Threading.Tasks.Task;

namespace TaskTimeTrackerIdentity.Web.Services
{
    public class MyTaskService : IMyTaskService
    {
        public MyTaskService()
        { }

        private readonly ApplicationDbContext _context = null!;

        public MyTaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MyTask>> GetTasksListByBoardId(int boardId)
        {
            List<MyTask> returnList = new();

            returnList = await _context.Tasks
                                        .Where(t => t.BoardId == boardId)
                                        .ToListAsync();

            return returnList;
        }

        public async Task<MyTask> GetTaskById(int taskId)
        {
            MyTask? myTask = new();

            myTask = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);

            return myTask;
        }

        public async Task<MyTask> CreateTask(MyTask myTask)
        {
            _context.Tasks.Add(myTask);
            await _context.SaveChangesAsync();
            return myTask;
        }

        public async ServiceTask UpdateTask(MyTask myTask)
        {
            _context.Tasks.Update(myTask);
            await _context.SaveChangesAsync();
        }

        public async ServiceTask DeleteTask(MyTask myTask)
        {
            _context.Tasks.Remove(myTask);
            await _context.SaveChangesAsync();
        }
    }
}