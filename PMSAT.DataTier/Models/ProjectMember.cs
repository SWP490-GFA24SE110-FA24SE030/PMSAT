using System;
using System.Collections.Generic;

namespace PMSAT.DataTier.Models;

public partial class ProjectMember
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public Guid? ProjectId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual Project? Project { get; set; }

    public virtual ICollection<Repository> Repositories { get; set; } = new List<Repository>();

    public virtual ICollection<RoleInProject> RoleInProjects { get; set; } = new List<RoleInProject>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual User? User { get; set; }
}
