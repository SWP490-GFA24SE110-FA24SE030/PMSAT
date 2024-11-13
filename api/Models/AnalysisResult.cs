using System;
using System.Collections.Generic;

namespace api.Models;

public partial class AnalysisResult
{
    public Guid Id { get; set; }

    public string? Coverage { get; set; }

    public string? CodeComplexity { get; set; }

    public string? QualityMetrics { get; set; }

    public string? RequirementError { get; set; }

    public string? SecurityError { get; set; }

    public string? SyntaxError { get; set; }

    public Guid? ProjectId { get; set; }

    public virtual Project? Project { get; set; }
}
