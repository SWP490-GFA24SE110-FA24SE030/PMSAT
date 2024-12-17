using System;
using System.Collections.Generic;

namespace api.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Repository> Repositories { get; set; } = new List<Repository>();

    public virtual ICollection<TaskP> TaskPAssignees { get; set; } = new List<TaskP>();

    public virtual ICollection<TaskP> TaskPReporters { get; set; } = new List<TaskP>();
}
