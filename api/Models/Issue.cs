using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Issue
{
    public Guid Id { get; set; }

    public string? Type { get; set; }

    public string? Detail { get; set; }

    public Guid? TaskId { get; set; }

    public virtual TaskP? Task { get; set; }
}
