using System.ComponentModel.DataAnnotations;

namespace InterviewPass.WebApi.Models.Question
{
    public class TrueFalseQuestionModel : QuestionModel
    {
        [Required]
        public bool? IsTrue { get; set; }
    }
}
