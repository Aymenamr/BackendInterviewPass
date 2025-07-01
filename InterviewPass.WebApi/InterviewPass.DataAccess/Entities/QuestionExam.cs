using System;
using System.Collections.Generic;

namespace InterviewPass.DataAccess.Entities;

public partial class QuestionExam
{
    public string IdExam { get; set; } = null!;

    public string IdQuestion { get; set; } = null!;

    public int Id { get; set; }

    
    public virtual Exam IdExamNavigation { get; set; } = null!;

    public virtual Question IdQuestionNavigation { get; set; } = null!;
}
