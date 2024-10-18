using System;
using System.Collections.Generic;

namespace PMSAT.DataTier.Models;

public partial class WorkFlow
{
    public Guid Id { get; set; }

    public Guid? TaskId { get; set; }

    public string? Status { get; set; }

    public string? FromStatus { get; set; }

    public string? ToStatus { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Task? Task { get; set; }
}
