using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Models;
namespace ToDoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public TasksController(AppDbContext _Context)
        {
            dbContext = _Context;
        }
        [HttpGet]
        public IActionResult GetTasks([FromQuery] bool? completed)
        {
            List<todolist> tasks = dbContext.Todolists.ToList();
            if (completed.HasValue)
            {
                tasks = tasks.Where(x => x.IsCompleted == completed.Value).ToList();
            }
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult AddTask(todolist task)
        {
            dbContext.Todolists.Add(task);
            dbContext.SaveChanges();
            return CreatedAtAction("GetById", new { id = task.id }, task);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            todolist task = dbContext.Todolists.FirstOrDefault(x => x.id == id);
            return Ok(task);
        }

        [HttpPut]
        public IActionResult UpdateTask(int id, todolist task)
        {
            todolist taskfromDB = dbContext.Todolists.FirstOrDefault(x => x.id == id);
            if (taskfromDB != null)
            {
                taskfromDB.title = task.title;
                taskfromDB.description = task.description;
                taskfromDB.DueDate = task.DueDate;
                taskfromDB.IsCompleted = task.IsCompleted;
                taskfromDB.Priority = task.Priority;
                dbContext.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound("Task not found");
            }

        }

        [HttpDelete]
        public IActionResult DeleteTask(int id)
        {
            todolist task = dbContext.Todolists.FirstOrDefault(x => x.id == id);
            if (task != null)
            {
                dbContext.Todolists.Remove(task);
                dbContext.SaveChanges();
                return Ok();
            }
            else return NotFound();
        }

        [HttpPut("{id}/complete")]
        public IActionResult MarkAsCompleted(int id)
        {
            todolist task = dbContext.Todolists.FirstOrDefault(x => x.id == id);
            if (task != null)
            {
                task.IsCompleted = true;
                dbContext.SaveChanges();
                return NoContent();
            }
            else return NotFound();
        }

        [HttpPut("{id}/Incomplete")]
        public IActionResult MarkAsIncompleted(int id)
        {
            todolist task = dbContext.Todolists.FirstOrDefault(x => x.id == id);
            if (task != null)
            {
                task.IsCompleted = false;
                dbContext.SaveChanges();
                return NoContent();
            }
            else return NotFound();
        }

        [HttpGet("date")]
        public IActionResult GetTasksByDate([FromQuery] DateTime date)
        {
            List<todolist> tasks = dbContext.Todolists.Where(x => x.DueDate == date).ToList();
            if (!tasks.Any())
            {
                return NotFound("No tasks found for the specified due date.");
            }
            return Ok(tasks);
        }

        [HttpGet("Priority")]
        public IActionResult GetTasksByProirity([FromQuery] string proirity)
        {
            List<todolist> tasks = dbContext.Todolists.Where(x => x.Priority == proirity).ToList();
            if (!tasks.Any())
            {
                return NotFound("No tasks found with that priority");
            }
            return Ok(tasks);
        }

        [HttpPut("{id}priority")]
        public IActionResult SetPriority(int id, [FromQuery] string priority)
        {
            todolist taskfromdb = dbContext.Todolists.FirstOrDefault(x => x.id == id);
            if (taskfromdb != null)
            {
                taskfromdb.Priority = priority;
                dbContext.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }
    }
}
