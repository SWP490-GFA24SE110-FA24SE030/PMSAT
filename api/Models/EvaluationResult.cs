using System;
using System.Collections.Generic;

namespace api.Models;

public partial class EvaluationResult
{
    public Guid Id { get; set; }

    public int? Score { get; set; }

    public string? WorkTrendAnalysis { get; set; }

    public string? ReviewerComments { get; set; }

    public string? Strengths { get; set; }

    public string? AreasForImprovement { get; set; }

    public Guid? ProjectMemberId { get; set; }

    public virtual ProjectMember? ProjectMember { get; set; }
}
