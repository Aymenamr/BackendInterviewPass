using InterviewPass.DataAccess.Entities;

public class SelectedPossibility
{
    public string AnswerId { get; set; }
    public string PossibilityId { get; set; }  // ✅ ضروري جدًا

    public virtual Answer Answer { get; set; }
    public virtual Possibility Possibility { get; set; }
}
