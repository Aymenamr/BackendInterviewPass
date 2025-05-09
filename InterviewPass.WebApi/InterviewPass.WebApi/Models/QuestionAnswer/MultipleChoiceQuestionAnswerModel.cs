using System.ComponentModel.DataAnnotations;
using InterviewPass.WebApi.Models.Question;

namespace InterviewPass.WebApi.Models.QuestionAnswer
{
    public class MultipleChoiceQuestionAnswerModel : QuestionAnswerModel
    {
        [Required]
        public bool AnswerChoice { get; set; }
        [Required]
        public PossibilityModel AnswerContent { get; set; }
    }
}
