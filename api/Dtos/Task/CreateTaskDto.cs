using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Task
{
    public class CreateTaskDto
    {
        public string? Type { get; set; }

    public string? Description { get; set; }
    }
}