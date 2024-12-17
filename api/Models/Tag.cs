using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Tag
{
    public Guid Id { get; set; }

    public string? Name { get; set; }
}
