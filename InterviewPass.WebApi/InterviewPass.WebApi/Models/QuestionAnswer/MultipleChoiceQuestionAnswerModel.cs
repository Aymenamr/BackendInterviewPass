using System.ComponentModel.DataAnnotations;
using InterviewPass.WebApi.Models.Question;
using InterviewPass.WebApi.Models;

namespace InterviewPass.WebApi.Models.QuestionAnswer
{
    public class MultipleChoiceQuestionAnswerModel : QuestionAnswerModel
    {
        [Required]
        public PossibilityModel AnswerContent { get; set; }
    }
}
