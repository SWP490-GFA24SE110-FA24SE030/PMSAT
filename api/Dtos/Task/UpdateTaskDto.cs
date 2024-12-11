namespace api.Dtos.Task
{
    public class UpdateTaskDto
    {
        public string? Status { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
