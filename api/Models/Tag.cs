using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Tag
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<TaskP> Tasks { get; set; } = new List<TaskP>();
}
