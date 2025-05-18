using System;
using System.Collections.Generic;

namespace InterviewPass.DataAccess.Entities;

public partial class Field
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
