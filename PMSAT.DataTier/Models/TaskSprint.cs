using System;
using System.Collections.Generic;

namespace PMSAT.DataTier.Models;

public partial class TaskSprint
{
    public Guid Id { get; set; }

    public Guid? TaskId { get; set; }

    public Guid? SprintId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Sprint? Sprint { get; set; }

    public virtual Task? Task { get; set; }
}
