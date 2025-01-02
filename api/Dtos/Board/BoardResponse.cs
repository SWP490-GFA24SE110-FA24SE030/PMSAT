using api.Dtos.Task;
using api.Models;

namespace api.Dtos.Board
{
    public class BoardResponse
    {
        public Guid Id { get; set; }
        public string? Status { get; set; }
        public virtual ICollection<TaskDto> TaskPs { get; set; } = new List<TaskDto>();
        public int? Orders { get; set; }
    }
}
