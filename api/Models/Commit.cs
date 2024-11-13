using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Commit
{
    public Guid Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Message { get; set; }

    public Guid? RepositoryId { get; set; }

    public virtual Repository? Repository { get; set; }
}
