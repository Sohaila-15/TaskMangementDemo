using Microsoft.AspNetCore.Mvc;
using TaskMangementDemo.Models;
using TaskMangementDemo.Data;

namespace TaskMangementDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskFileService _fileService;

        public TasksController(TaskFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public ActionResult<List<TaskItem>> GetAll()
        {
            return _fileService.LoadTasks();
        }

        [HttpGet("{id}")]
        public ActionResult<TaskItem> Get(int id)
        {
            var tasks = _fileService.LoadTasks();
            var task = tasks.FirstOrDefault(x => x.Id == id);

            if (task == null) return NotFound();

            return task;
        }

        [HttpPost]
        public ActionResult<TaskItem> Create(TaskItem task)
        {
            var tasks = _fileService.LoadTasks();

            task.Id = tasks.Count == 0 ? 1 : tasks.Max(t => t.Id) + 1;

            tasks.Add(task);
            _fileService.SaveTasks(tasks);

            return Ok(task);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TaskItem updatedTask)
        {
            var tasks = _fileService.LoadTasks();
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null) return NotFound();

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;

            _fileService.SaveTasks(tasks);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tasks = _fileService.LoadTasks();
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null) return NotFound();

            tasks.Remove(task);
            _fileService.SaveTasks(tasks);

            return NoContent();
        }
    }
}
