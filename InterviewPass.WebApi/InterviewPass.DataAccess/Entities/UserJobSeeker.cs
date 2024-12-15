using System;
using System.Collections.Generic;

namespace InterviewPass.DataAccess.Entities;

public partial class UserJobSeeker
{
    public string? Login { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Password { get; set; }

    public int? LevelOfExperience { get; set; }

    public string Id { get; set; } = null!;

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual ICollection<SkillBySeeker> SkillBySeekers { get; set; } = new List<SkillBySeeker>();
}
