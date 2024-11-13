using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Feedback
{
    public Guid Id { get; set; }

    public string? Type { get; set; }

    public string? Detail { get; set; }

    public Guid? UserId { get; set; }

    public virtual User? User { get; set; }
}
