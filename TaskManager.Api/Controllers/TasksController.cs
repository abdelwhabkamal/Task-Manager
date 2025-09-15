using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskRepository _tasks;

    public TasksController(ITaskRepository tasks)
    {
        _tasks = tasks;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _tasks.GetAllAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _tasks.GetByIdAsync(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpGet("project/{projectId}")]
    public async Task<IActionResult> GetByProject(int projectId)
    {
        var tasks = await _tasks.GetTasksByProjectIdAsync(projectId);
        return Ok(tasks);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId)
    {
        var tasks = await _tasks.GetTasksByUserIdAsync(userId);
        return Ok(tasks);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(TaskItem task)
    {
        await _tasks.AddAsync(task);
        await _tasks.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TaskItem updated)
    {
        var task = await _tasks.GetByIdAsync(id);
        if (task == null) return NotFound();

        task.Title = updated.Title;
        task.Description = updated.Description;
        task.Status = updated.Status;
        task.Priority = updated.Priority;
        task.AssignedTo = updated.AssignedTo;
        task.DueDate = updated.DueDate;

        _tasks.Update(task);
        await _tasks.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _tasks.GetByIdAsync(id);
        if (task == null) return NotFound();

        _tasks.Remove(task);
        await _tasks.SaveChangesAsync();
        return NoContent();
    }
}
