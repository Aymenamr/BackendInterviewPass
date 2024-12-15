using System;
using System.Collections.Generic;

namespace InterviewPass.DataAccess.Entities;

public partial class Possibility
{
    public string Id { get; set; } = null!;

    public string? QuestionId { get; set; }

    public bool? IsTheCorrectAnswer { get; set; }

    public string? Content { get; set; }

    public virtual Question? Question { get; set; }

    public virtual ICollection<SelectedPossibility> SelectedPossibilities { get; set; } = new List<SelectedPossibility>();
}
