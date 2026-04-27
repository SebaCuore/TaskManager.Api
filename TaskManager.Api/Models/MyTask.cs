using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Api.Models
{
    public class MyTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsCompleted { get; set; }
        public Priority Priority { get; set; }


        public MyTask(int id, string name, bool isCompleted, Priority priority)
        {
            this.Id = id;
            this.Name = name;
            this.IsCompleted = isCompleted;
            this.Priority = priority;
        }


    }
}
