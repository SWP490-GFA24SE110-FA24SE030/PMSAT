using System;
using System.Collections.Generic;

namespace PMSAT.DataTier.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string? Role { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<IntegrationConfig> IntegrationConfigs { get; set; } = new List<IntegrationConfig>();

    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
}
