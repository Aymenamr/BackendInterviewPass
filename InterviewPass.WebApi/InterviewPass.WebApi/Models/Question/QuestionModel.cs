using System.ComponentModel.DataAnnotations;
using InterviewPass.WebApi.Enums;

namespace InterviewPass.WebApi.Models.Question
{
    public class QuestionModel
    {

        public string Id { get; set; } = null!;
        [Required]
        public string? Content { get; set; }
        [Required]
        public double? Score { get; set; }
        [Required]
        public QuestionType? Type { get; set; }

        public string? SkillId { get; set; }
    }
}
