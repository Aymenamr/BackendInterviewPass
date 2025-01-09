using System;
using System.Collections.Generic;

namespace InterviewPass.DataAccess.Entities;

public partial class Job
{
    public string Id { get; set; } = null!;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? ImagePath { get; set; }

    public int? EmploymentTypeId { get; set; }

    public int? Experience { get; set; }

    public int? SalaryType { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual EmploymentType? EmploymentType { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<JobFile> JobFiles { get; set; } = new List<JobFile>();

    public virtual ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
}
