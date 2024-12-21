using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Board
{
    public Guid Id { get; set; }

    public int? Orders { get; set; }

    public string? Status { get; set; }

    public Guid? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public virtual ICollection<TaskP> TaskPs { get; set; } = new List<TaskP>();
}
