using System;
using System.Collections.Generic;

namespace PMSAT.DataTier.Models;

public partial class ProjectAnalysisResult
{
    public Guid Id { get; set; }

    public Guid? ProjectId { get; set; }

    public string? Converage { get; set; }

    public string? QualityMetrics { get; set; }

    public string? Complexity { get; set; }

    public string? SyntaxError { get; set; }

    public string? SecurityError { get; set; }

    public string? RequirementError { get; set; }

    public virtual Project? Project { get; set; }
}
