using System.ComponentModel.DataAnnotations;
using InterviewPass.WebApi.Models.Question;
using InterviewPass.WebApi.Models.User;

namespace InterviewPass.WebApi.Models
{
    public class ExamModel
    {
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Author { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "MinScore must be greater than 0.")]
        public int MinScore { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "MaxScore must be greater than 0.")]
        public int MaxScore { get; set; }

        [Range(1, 100, ErrorMessage = "MaxScore must be greater than 0.")]
        public int? DeadLineInNbrOfDays { get; set; }
        [Required]
        public short NbrOfQuestion { get; set; }

        public List<QuestionModel>? Questions {  get; set; }
    }
}
