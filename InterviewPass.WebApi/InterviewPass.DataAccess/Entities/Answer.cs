using System.Collections.Generic;

namespace InterviewPass.DataAccess.Entities
{
    public partial class Answer
    {
        public string Id { get; set; } = null!;

        public string? ExamId { get; set; }
        public string? QuestionId { get; set; }
        public string? Type { get; set; }
        public double? ObtainedScore { get; set; }
        public string? UserId { get; set; }
        public bool? IsTrueSelected { get; set; }
        public string? TextAnswer { get; set; }
        public byte[]? ImageAnswer { get; set; }
        public byte[]? ZipAnswer { get; set; }
        public string? GitHubLink { get; set; }
        public string? ResultId { get; set; }

        //  (Navigation Properties)
        public virtual Exam? Exam { get; set; }
        public virtual Question? Question { get; set; }
        public virtual UserJobSeeker? User { get; set; }
        public virtual Result? Result { get; set; }

        public virtual ICollection<SelectedPossibility> SelectedPossibilities { get; set; } = new List<SelectedPossibility>();

    }
}
