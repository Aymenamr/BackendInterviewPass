using System;
using System.Collections.Generic;

namespace InterviewPass.DataAccess.Entities;

public partial class JobSkill
{
    public string Id { get; set; } = null!;

    public string? JobId { get; set; }

    public string? SkillId { get; set; }

    public virtual Job? Job { get; set; }
}
