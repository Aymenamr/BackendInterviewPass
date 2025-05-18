using System.ComponentModel.DataAnnotations;
using InterviewPass.WebApi.Models.Question;

namespace InterviewPass.WebApi.Models
{
    public class ExamModel
    {
        [RegularExpression(@"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$", ErrorMessage = "ID must be GUID.")]
        public string? Id { get; set; }
        [Required]
        [MaxLength(length:100,ErrorMessage ="Exam name is too large.")]
        public string Name { get; set; }
        [Required]
        [MaxLength(length: 10000, ErrorMessage = "Exam description is too large.")]
        public string Description { get; set; }
        [Required]
        [RegularExpression(@"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$", ErrorMessage = "Author ID must be GUID.")]
        public string Author { get; set; }
        [Required]
        [Range(1, short.MaxValue, ErrorMessage = "MinScore must be greater than 0.")]
        public double MinScore { get; set; }

        [Range(1, short.MaxValue, ErrorMessage = "MaxScore must be greater than 0.")]
        public double? MaxScore { get; set; }
        [Required]
        [Range(1, 365, ErrorMessage = "Dead line days must be greater than 0 and less than 365.")]
        public int? DeadLineInNbrOfDays { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Number of questions must be greater than 0")]
        public short NbrOfQuestion { get; set; }

        public List<QuestionModel>? Questions {  get; set; }
    }
}
