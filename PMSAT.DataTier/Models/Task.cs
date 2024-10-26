using System;
using System.Collections.Generic;

namespace PMSAT.DataTier.Models;

public partial class Task
{
    public Guid Id { get; set; }

    public Guid? BacklogId { get; set; }

    public Guid? ProjectMemberId { get; set; }

    public string? Type { get; set; }

    public string? Description { get; set; }

    public int? Priority { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ProjectMember? ProjectMember { get; set; }

    public virtual ICollection<TaskSprint> TaskSprints { get; set; } = new List<TaskSprint>();

    public virtual ICollection<WorkFlow> WorkFlows { get; set; } = new List<WorkFlow>();
}
