namespace api.Dtos.Task
{
    public class UpdateTaskStatusDto
    {
        public required string NewStatus { get; set; } // e.g., "To-Do", "In Progress", "Done"
    }
}
