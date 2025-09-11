using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<Project?> GetProjectWithTasksAsync(int id);
    }
}
