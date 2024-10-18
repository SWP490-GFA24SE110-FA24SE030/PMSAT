using System;
using System.Collections.Generic;

namespace PMSAT.DataTier.Models;

public partial class BackLog
{
    public Guid Id { get; set; }

    public Guid? ProjectId { get; set; }

    public string? Description { get; set; }

    public virtual Project? Project { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
