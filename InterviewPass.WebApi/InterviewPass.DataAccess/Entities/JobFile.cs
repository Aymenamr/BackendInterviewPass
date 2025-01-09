using System;
using System.Collections.Generic;

namespace InterviewPass.DataAccess.Entities;

public partial class JobFile
{
    public string Id { get; set; } = null!;

    public string? JobId { get; set; }

    public string? FilePath { get; set; }

    public virtual Job? Job { get; set; }
}
