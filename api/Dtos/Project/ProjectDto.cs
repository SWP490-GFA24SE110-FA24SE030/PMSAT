using api.Dtos.Sprint;
using api.Dtos.Task;
using api.Models;

namespace api.Dtos.Project
{
    public class ProjectDto
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? Status { get; set; }
        
        public virtual ICollection<TaskDto> TaskPs { get; set; } = new List<TaskDto>();

        public virtual ICollection<SprintDto> Sprints { get; set; } = new List<SprintDto>();
    }
}