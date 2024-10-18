using System;
using System.Collections.Generic;

namespace PMSAT.DataTier.Models;

public partial class RoleInProject
{
    public Guid Id { get; set; }

    public Guid ProjectRoleId { get; set; }

    public Guid ProjectMemberId { get; set; }

    public virtual ProjectMember ProjectMember { get; set; } = null!;

    public virtual ProjectRole ProjectRole { get; set; } = null!;
}
