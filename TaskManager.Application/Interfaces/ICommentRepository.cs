using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsByTaskIdAsync(int taskId);
    }
}
