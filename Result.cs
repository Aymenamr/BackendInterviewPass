using System;
using System.Collections.Generic;
namespace InterviewPass.DataAccess.Entities;

/*هذا الكلاس يمثل الجدول في قاعدة البيانات، ويحتوي على خصائص تمثل النتيجة.*/
public partial class Result
{
    public ICollection<QuestionResults> QuestionResults { get; set; } = new List<QuestionResults>();
    public string Id { get; set; } = null!;

    public string? Status { get; set; }

    public string? ExamId { get; set; }

    public double? Score { get; set; }

    public string? UserId { get; set; }

    public DateTime? DeadLine { get; set; }

    public DateTime StartDate { get; set; }


    public bool? CandidateSucceeded { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Exam? Exam { get; set; }

    public virtual UserJobSeeker? User { get; set; }
    public string Name { get; set; }
    public object QuestionExams { get; set; }
}
