using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskRepository _tasks;
    private readonly IMapper _mapper;

    public TasksController(ITaskRepository tasks, IMapper mapper)
    {
        _tasks = tasks;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _tasks.GetAllAsync();
        var result = _mapper.Map<IEnumerable<TaskDto>>(tasks);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _tasks.GetByIdAsync(id);
        if (task == null) return NotFound();
        return Ok(_mapper.Map<TaskDto>(task));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto dto)
    {
        var task = _mapper.Map<TaskItem>(dto);
        await _tasks.AddAsync(task);
        await _tasks.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, _mapper.Map<TaskDto>(task));
    }
}
