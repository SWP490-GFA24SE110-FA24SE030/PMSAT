using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Sprint
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }

    public Guid? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public virtual ICollection<TaskSprint> TaskSprints { get; set; } = new List<TaskSprint>();
}
