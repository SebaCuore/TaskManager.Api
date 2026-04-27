using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Api.Models;

namespace TaskManager.Api.Services
{
    public class TaskManagerService
    {
        private static List<MyTask> _list = new List<MyTask>();

        public static void Initialize()
        {
            _list = FileService.LoadFromFile();
        }

        public static MyTask AddTask(string name, Priority priority)
        {
            int newId = _list.Count > 0 ? _list.Max(t => t.Id) + 1 : 1;
            var task = new MyTask(newId, name, false, priority);
            _list.Add(task);

            FileService.SaveToFile(_list);
            return task;
        }

        public static List<MyTask> GetAllTasks()
        {
            return _list;
        }

        public static MyTask GetTaskById(int taskId)
        {
            int taskIndex = _list.FindIndex(t => t.Id == taskId);
            if (taskIndex != -1)
            {
                return _list[taskIndex];
            }
            return null;
        }
        public static bool ModifyTaskState(int id, MyTask updatedTask)
        {
            var task = GetTaskById(id);
            if (task == null)
            {
                return false;
            }
            task.Name = updatedTask.Name;
            task.IsCompleted = updatedTask.IsCompleted;
            task.Priority = updatedTask.Priority;

            FileService.SaveToFile(_list);
            return true;


        }

        public static bool DeleteTask(int taskId)
        {
            int taskIndex = _list.FindIndex(t => t.Id == taskId);
            if (taskIndex != -1)
            {
                _list.RemoveAt(taskIndex);
                FileService.SaveToFile(_list);
                return true;
            }
            return false;
        }
    }
}
