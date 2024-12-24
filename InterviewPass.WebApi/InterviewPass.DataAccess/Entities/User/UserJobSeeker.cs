using System;
using System.Collections.Generic;

namespace InterviewPass.DataAccess.Entities;

public partial class UserJobSeeker : User
{ 
    public int? LevelOfExperience { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual ICollection<SkillBySeeker> SkillBySeekers { get; set; } = new List<SkillBySeeker>();
}
