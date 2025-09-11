using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly TaskManagerDbContext _context;
        public ProjectRepository(TaskManagerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Project?> GetProjectWithTasksAsync(int id)
        {
            return await _context.Projects
                                 .Include(p => p.Tasks)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
