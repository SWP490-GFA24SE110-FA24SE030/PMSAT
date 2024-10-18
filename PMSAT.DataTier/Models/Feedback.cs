using System;
using System.Collections.Generic;

namespace PMSAT.DataTier.Models;

public partial class Feedback
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string? Type { get; set; }

    public string? Content { get; set; }

    public virtual User? User { get; set; }
}
