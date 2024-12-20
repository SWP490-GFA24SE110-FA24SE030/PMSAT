using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Project
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<AnalysisResult> AnalysisResults { get; set; } = new List<AnalysisResult>();

    public virtual ICollection<Board> Boards { get; set; } = new List<Board>();

    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

    public virtual ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();

    public virtual ICollection<TaskP> TaskPs { get; set; } = new List<TaskP>();
}
