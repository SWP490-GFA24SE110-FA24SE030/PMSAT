using System;
using System.Collections.Generic;

namespace api.Models;

public partial class TaskP
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? Priority { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Updated { get; set; }

    public Guid? ReporterId { get; set; }

    public Guid? AssigneeId { get; set; }

    public Guid? ProjectId { get; set; }

    public Guid? SprintId { get; set; }

    public Guid? StatusId { get; set; }

    public virtual User? Assignee { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public virtual Project? Project { get; set; }

    public virtual User? Reporter { get; set; }

    public virtual Sprint? Sprint { get; set; }

    public virtual Board? Status { get; set; }

    public virtual ICollection<Workflow> Workflows { get; set; } = new List<Workflow>();
}
