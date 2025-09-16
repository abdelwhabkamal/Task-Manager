namespace TaskManager.Application.DTOs
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = "ToDo";
        public string Priority { get; set; } = "Medium";
        public int ProjectId { get; set; }
        public int? AssignedTo { get; set; }
        public DateTime DueDate { get; set; }
    }
}
