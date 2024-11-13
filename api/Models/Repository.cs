using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Repository
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Owner { get; set; }

    public string? Url { get; set; }

    public Guid? ProjectMemberId { get; set; }

    public virtual ICollection<Commit> Commits { get; set; } = new List<Commit>();

    public virtual ProjectMember? ProjectMember { get; set; }

    public virtual ICollection<PullRequest> PullRequests { get; set; } = new List<PullRequest>();
}
