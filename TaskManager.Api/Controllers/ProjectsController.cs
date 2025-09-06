using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure;
using TaskManager.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly TaskManagerDbContext _db;
    public ProjectsController(TaskManagerDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Get() =>
        Ok(await _db.Projects.Include(p => p.Tasks).ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Create(Project project)
    {
        _db.Projects.Add(project);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = project.Id }, project);
    }
}
