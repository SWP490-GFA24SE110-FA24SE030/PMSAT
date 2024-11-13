using System;
using System.Collections.Generic;

namespace api.Models;

public partial class TaskSprint
{
    public Guid Id { get; set; }

    public DateTime? UpdateStartDate { get; set; }

    public DateTime? UpdatedEndDate { get; set; }

    public Guid? SprintId { get; set; }

    public Guid? TaskId { get; set; }

    public virtual Sprint? Sprint { get; set; }

    public virtual TaskP? Task { get; set; }
}
