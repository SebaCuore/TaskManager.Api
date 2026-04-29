using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Api.Data;
using TaskManager.Api.Models;

namespace TaskManager.Api.Services
{
    public class TaskManagerService
    {
        private readonly TaskManagerContext _context;
        //private static List<MyTask> _list = new List<MyTask>();

        //public static void Initialize()
        //{
        //    _list = FileService.LoadFromFile();
        //}
        public TaskManagerService(TaskManagerContext context)
        {
            _context = context;
        }

        public async Task<MyTask> AddTaskAsync(string name, Priority priority)
        {
            var newTask = new MyTask(0, name, false, priority);
            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();
            return newTask;
        }

        public async Task<List<MyTask>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<MyTask> GetTaskById(int taskId)
        {
            return await _context.Tasks.FindAsync(taskId);
        }
        public async Task<bool> ModifyTaskState(int id, MyTask updatedTask)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return false;
            }
            task.Name = updatedTask.Name;
            task.IsCompleted = updatedTask.IsCompleted;
            task.Priority = updatedTask.Priority;
            await _context.SaveChangesAsync();
            return true;


        }

        public async Task<bool> DeleteTask(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId); ;
            if (task == null)
            {
                return false;
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
