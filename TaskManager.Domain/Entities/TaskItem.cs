using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TaskManager.Domain.Entities
{
    public enum TaskStatus { ToDo, InProgress, Done }
    public enum Priority { Low, Medium, High }
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.ToDo;
        public Priority Priority { get; set; } = Priority.Medium;

        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public int? AssignedToId { get; set; }
        public User? AssignedTo { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
