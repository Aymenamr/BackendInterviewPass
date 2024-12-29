using System;
using System.Collections.Generic;

namespace InterviewPass.DataAccess.Entities;

public partial class Skill
{
    public string Id { get; set; }

    public string Name { get; set; } = null!;

    public string? FieldId { get; set; }

    public virtual Field? Field { get; set; }

    public virtual ICollection<SkillBySeeker> SkillBySeekers { get; set; } = new List<SkillBySeeker>();
}
