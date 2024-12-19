using System;
using System.Collections.Generic;

namespace api.Models;

public partial class ProjectMember
{
    public string? Role { get; set; }

    public Guid UserId { get; set; }

    public Guid ProjectId { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
