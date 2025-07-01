using System;
using System.Collections.Generic;

namespace InterviewPass.DataAccess.Entities;

public partial class SelectedPossibility
{
    public int Id { get; set; }

    public string? IdPossibility { get; set; }

    public string? IdAnswer { get; set; }

    public virtual Answer? IdAnswerNavigation { get; set; }

    public virtual Possibility? IdPossibilityNavigation { get; set; }
    public string PossibilityId { get; set; }
}
