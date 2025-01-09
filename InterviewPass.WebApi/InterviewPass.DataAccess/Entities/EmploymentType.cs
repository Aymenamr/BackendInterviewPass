using System;
using System.Collections.Generic;

namespace InterviewPass.DataAccess.Entities;

public partial class EmploymentType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
