using System.ComponentModel.DataAnnotations;
using InterviewPass.WebApi.Models.Question;
using InterviewPass.WebApi.Models;

namespace InterviewPass.WebApi.Models.QuestionAnswer
{
    public class QuestionAnswerModel
    {
        public string? Id { get; set; } = null!;
        [Required]
        public string? Content { get; set; }
        [Required]
        public double? Score { get; set; }
    }
}
