using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectRepository _projects;
    private readonly IMapper _mapper;

    public ProjectsController(IProjectRepository projects, IMapper mapper)
    {
        _projects = projects;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectCreateDto projectDto)
    {
        var project = _mapper.Map<Project>(projectDto);

        await _projects.AddAsync(project);
        await _projects.SaveChangesAsync();

        var projectRead = _mapper.Map<ProjectReadDto>(project);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, projectRead);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var project = await _projects.GetProjectWithTasksAsync(id);
        if (project == null) return NotFound();

        var projectRead = _mapper.Map<ProjectReadDto>(project);
        return Ok(projectRead);
    }
}
