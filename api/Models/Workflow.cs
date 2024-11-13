using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Workflow
{
    public Guid Id { get; set; }

    public string? OldStatus { get; set; }

    public string? CurrentStatus { get; set; }

    public string? NewStatus { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? TaskId { get; set; }

    public virtual TaskP? Task { get; set; }
}
