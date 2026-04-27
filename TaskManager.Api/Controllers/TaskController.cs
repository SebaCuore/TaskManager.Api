using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Models;
using TaskManager.Api.Services;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<MyTask>> GetAllTasks()
        {
            return Ok(TaskManagerService.GetAllTasks());
        }

        [HttpGet("{id}")]
        public ActionResult<MyTask> GetTask(int id)
        {
            var task = TaskManagerService.GetTaskById(id);
            if (task != null)
            {
                return Ok(task);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<MyTask> AddTask(MyTask newTask)
        {
            if (newTask == null)
            {
                return BadRequest();
            }

            var createdTask = TaskManagerService.AddTask(newTask.Name, newTask.Priority);
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public ActionResult ModifyTaskState(int id, MyTask updatedTask)
        {
            if (TaskManagerService.ModifyTaskState(id, updatedTask))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            if (TaskManagerService.DeleteTask(id))
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}