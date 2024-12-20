﻿using System;
using System.Collections.Generic;

namespace api.Models;

public partial class PullRequest
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Body { get; set; }

    public string? Url { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? MergedAt { get; set; }

    public string? Status { get; set; }

    public Guid? RepositoryId { get; set; }

    public virtual Repository? Repository { get; set; }
}
