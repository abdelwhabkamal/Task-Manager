using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository _comments;

    public CommentsController(ICommentRepository comments)
    {
        _comments = comments;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _comments.GetAllAsync();
        return Ok(comments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var comment = await _comments.GetByIdAsync(id);
        if (comment == null) return NotFound();
        return Ok(comment);
    }

    [HttpGet("task/{taskId}")]
    public async Task<IActionResult> GetByTask(int taskId)
    {
        var comments = await _comments.GetCommentsByTaskIdAsync(taskId);
        return Ok(comments);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Comment comment)
    {
        comment.CreatedAt = DateTime.UtcNow;

        await _comments.AddAsync(comment);
        await _comments.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Comment updated)
    {
        var comment = await _comments.GetByIdAsync(id);
        if (comment == null) return NotFound();

        comment.Content = updated.Content;

        _comments.Update(comment);
        await _comments.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var comment = await _comments.GetByIdAsync(id);
        if (comment == null) return NotFound();

        _comments.Remove(comment);
        await _comments.SaveChangesAsync();
        return NoContent();
    }
}
