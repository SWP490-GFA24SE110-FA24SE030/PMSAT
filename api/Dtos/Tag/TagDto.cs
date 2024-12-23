using api.Models;

namespace api.Dtos.Tag
{
    public class TagDto
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<TaskP> Tasks { get; set; } = new List<TaskP>();
    }
}
