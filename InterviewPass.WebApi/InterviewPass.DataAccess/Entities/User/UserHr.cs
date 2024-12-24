using System;
using System.Collections.Generic;

namespace InterviewPass.DataAccess.Entities;

public partial class UserHr : User
{  
    public string? Company { get; set; }
    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
