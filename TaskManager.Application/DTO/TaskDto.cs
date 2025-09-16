namespace TaskManager.Application.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Status { get; set; } = "ToDo";
        public string Priority { get; set; } = "Medium";
        public int ProjectId { get; set; }
        public int? AssignedTo { get; set; }
        public DateTime DueDate { get; set; }
    }
}
