using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private static readonly List<TaskItem> Tasks = new()
    {
        new TaskItem { Id = 1, Title = "Learn Web API", Description = "Follow tutorial", IsCompleted = false },
        new TaskItem { Id = 2, Title = "Create first task", Description = "Add TaskItem model", IsCompleted = true }
    };

    [HttpGet]
    public ActionResult<IEnumerable<TaskItem>> GetAll()
    {
        return Ok(Tasks);
    }


    [HttpPost]
    public ActionResult<TaskItem> Create(TaskItem newTask)
    {
        // Find next Id
        int nextId = Tasks.Count == 0 ? 1 : Tasks.Max(t => t.Id) + 1;
        newTask.Id = nextId;

        // Add to the list
        Tasks.Add(newTask);

        // Return 201 Created with the new task
        return CreatedAtAction(nameof(GetAll), new { id = newTask.Id }, newTask);
    }



    [HttpPut("{id}")]
    public IActionResult Update(int id, TaskItem updatedTask)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
            return NotFound();  // 404 if not found

        // Update fields
        task.Title = updatedTask.Title;
        task.Description = updatedTask.Description;
        task.IsCompleted = updatedTask.IsCompleted;

        return NoContent();  // 204 success, no body
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
            return NotFound();

        Tasks.Remove(task);
        return NoContent();  // 204 success
    }
}