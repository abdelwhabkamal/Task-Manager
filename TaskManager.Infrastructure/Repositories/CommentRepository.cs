using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly TaskManagerDbContext _context;

        public CommentRepository(TaskManagerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByTaskIdAsync(int taskId)
        {
            return await _context.Comments
                                 .Where(c => c.TaskItemId == taskId)
                                 .Include(c => c.CreatedBy) 
                                 .ToListAsync();
        }
    }
}
