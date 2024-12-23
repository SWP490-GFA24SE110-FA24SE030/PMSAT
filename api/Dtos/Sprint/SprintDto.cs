using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Task;

namespace api.Dtos.Sprint
{
    public class SprintDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Status { get; set; }
        public virtual ICollection<TaskDto> TaskPs { get; set; } = new List<TaskDto>();
    }
}