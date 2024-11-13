using System;
using System.Collections.Generic;

namespace api.Models;

public partial class ProjectMember
{
    public Guid Id { get; set; }

    public string? Role { get; set; }

    public Guid? UserId { get; set; }

    public Guid? ProjectId { get; set; }

    public virtual ICollection<EvaluationResult> EvaluationResults { get; set; } = new List<EvaluationResult>();

    public virtual Project? Project { get; set; }

    public virtual ICollection<Repository> Repositories { get; set; } = new List<Repository>();

    public virtual ICollection<TaskP> TaskPs { get; set; } = new List<TaskP>();

    public virtual User? User { get; set; }
}
