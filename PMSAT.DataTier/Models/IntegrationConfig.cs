using System;
using System.Collections.Generic;

namespace PMSAT.DataTier.Models;

public partial class IntegrationConfig
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string? ToolName { get; set; }

    public string? Settings { get; set; }

    public string? ApiKey { get; set; }

    public string? Url { get; set; }

    public virtual User? User { get; set; }
}
