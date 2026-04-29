using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Models;
using TaskManager.Api.Services;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskManagerService _service;
        public TaskController(TaskManagerService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<List<MyTask>>> GetAllTasks()
        {
            var tasks = await _service.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MyTask>> GetTask(int id)
        {
            var task = await _service.GetTaskById(id);
            if (task != null)
            {
                return Ok(task);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<MyTask>> AddTask(MyTask newTask)
        {
            if (newTask == null)
            {
                return BadRequest();
            }

            var createdTask = await _service.AddTaskAsync(newTask.Name, newTask.Priority);
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ModifyTaskState(int id, MyTask updatedTask)
        {
            if (updatedTask.Id != id)
            {
                return BadRequest();
            }

            if (await _service.ModifyTaskState(id, updatedTask))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            if (await _service.DeleteTask(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}