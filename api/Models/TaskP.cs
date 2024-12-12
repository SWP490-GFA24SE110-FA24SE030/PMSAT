using System;
using System.Collections.Generic;

namespace api.Models;

public partial class TaskP
{
    public Guid Id { get; set; }

    public string? Status { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? Priority { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Guid? ProjectMemberId { get; set; }

    public Guid? ProjectId { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public virtual Project? Project { get; set; }

    public virtual ProjectMember? ProjectMember { get; set; }

    public virtual ICollection<TaskSprint> TaskSprints { get; set; } = new List<TaskSprint>();

    public virtual ICollection<Workflow> Workflows { get; set; } = new List<Workflow>();
}
