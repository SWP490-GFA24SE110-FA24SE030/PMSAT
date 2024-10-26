using System;
using System.Collections.Generic;

namespace PMSAT.DataTier.Models;

public partial class Project
{
    public Guid Id { get; set; }

    public Guid? Name { get; set; }

    public Guid? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<ProjectAnalysisResult> ProjectAnalysisResults { get; set; } = new List<ProjectAnalysisResult>();

    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

    public virtual ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();
}
