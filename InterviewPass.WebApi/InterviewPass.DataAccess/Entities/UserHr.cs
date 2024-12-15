using System;
using System.Collections.Generic;

namespace InterviewPass.DataAccess.Entities;

public partial class UserHr
{
    public string? Login { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? PasswordHash { get; set; }

    public string? Company { get; set; }

    public string Id { get; set; } = null!;

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
