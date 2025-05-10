using System.ComponentModel.DataAnnotations;

namespace InterviewPass.WebApi.Models.QuestionAnswer
{
    public class TrueFalseQuestionAnswerModel : QuestionAnswerModel
    {
        [Required]
        public bool? IsTrue { get; set; }
    }
}
