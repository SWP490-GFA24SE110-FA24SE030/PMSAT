using System;
using System.Collections.Generic;

namespace PMSAT.DataTier.Models;

public partial class Commit
{
    public Guid Id { get; set; }

    public Guid? RepositoryId { get; set; }

    public string? Message { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Repository? Repository { get; set; }
}
