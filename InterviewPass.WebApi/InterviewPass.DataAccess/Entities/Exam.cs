using System;
using System.Collections.Generic;

namespace InterviewPass.DataAccess.Entities;

public partial class Exam
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public double? MinScore { get; set; }

    public double? MaxScore { get; set; }

    public int? DeadLineInNbrOfDays { get; set; }

    public string? CreatedBy { get; set; }

    public int? NbrOfQuestion { get; set; }

    public DateTime? CreationDate { get; set; }

    public string? JobId { get; set; }

    public virtual Job? Job { get; set; }
}
