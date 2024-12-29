using System;
using System.Collections.Generic;

namespace InterviewPass.DataAccess.Entities;

public partial class Question
{
    public string Id { get; set; } = null!;

    public string? Content { get; set; }

    public double? Score { get; set; }

    public string? Type { get; set; }

    public string? SkillId { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<Possibility> Possibilities { get; set; } = new List<Possibility>();

    public virtual ICollection<QuestionExam> QuestionExams { get; set; } = new List<QuestionExam>();

    public virtual Skill? Skill { get; set; }
}
