using api.Dtos.Tag;
using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Task
{
    public class TaskDto
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int? Priority { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        public string? Status { get; set; }

        public virtual ICollection<TagDto> Tags { get; set; } = new List<TagDto>();
    }
}