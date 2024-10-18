using System;
using System.Collections.Generic;

namespace PMSAT.DataTier.Models;

public partial class ProjectRole
{
    public Guid Id { get; set; }

    public string? RoleName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<RoleInProject> RoleInProjects { get; set; } = new List<RoleInProject>();
}
