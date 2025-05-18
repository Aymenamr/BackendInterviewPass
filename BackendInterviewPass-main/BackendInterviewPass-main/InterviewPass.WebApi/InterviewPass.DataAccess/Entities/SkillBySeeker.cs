using System;
using System.Collections.Generic;

namespace InterviewPass.DataAccess.Entities;

public partial class SkillBySeeker
{
    public int Id { get; set; }

    public string? SkillId { get; set; }

    public string? JobSeekerId { get; set; }

    public virtual UserJobSeeker? JobSeeker { get; set; }

    public virtual Skill? Skill { get; set; }
}
