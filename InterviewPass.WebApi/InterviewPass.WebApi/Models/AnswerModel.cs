using System.ComponentModel.DataAnnotations;
using InterviewPass.DataAccess.Entities;


namespace InterviewPass.WebApi.Models
{
    public class AnswerModel
    {
        [RegularExpression(@"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$", ErrorMessage = "ID must be GUID.")]
        public String? Id { get; set; } = null!;
        public string? ExamId { get; set; }//
        public string? QuestionId { get; set; }//
        //[Required]
        //[MaxLength(length: 100, ErrorMessage = "Exam Answer name is too large.")]       
        //public String Name { get; set; }
        [Required]
        public string? Type { get; set; }
        //[Required]
        //public String? Content { get; set; }
        [Required]
        public double? ObtainedScore { get; set; }//
        public string? UserId { get; set; }//
        public bool? IsTrueSelected { get; set; }//

        public string? TextAnswer { get; set; }//

        public byte[]? ImageAnswer { get; set; }//

        public byte[]? ZipAnswer { get; set; }//

        public string? GitHubLink { get; set; }//

        public string? ResultId { get; set; }//

        public virtual Exam? Exam { get; set; }//

        public virtual Question? Question { get; set; }//
        //[Required]
        //[Range(1, short.MaxValue, ErrorMessage = "MinScore must be greater than 0.")]
        //public double MinScore { get; set; }

        //[Range(1, short.MaxValue, ErrorMessage = "MaxScore must be greater than 0.")]
        //public double? MaxScore { get; set; }
        //public short NbrOfAnswer { get; set; }
        public virtual ICollection<SelectedPossibility> SelectedPossibilities { get; set; } = new List<SelectedPossibility>();//
        public virtual UserJobSeeker? User { get; set; }//
        public List<AnswerModel>? Answers { get; set; }
    }
}
