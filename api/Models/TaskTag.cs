using System;
using System.Collections.Generic;

namespace api.Models;

public partial class TaskTag
{
    public Guid? TaskId { get; set; }

    public Guid? TagId { get; set; }

    public virtual Tag? Tag { get; set; }

    public virtual TaskP? Task { get; set; }
}
