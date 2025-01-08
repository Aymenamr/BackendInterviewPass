using System.ComponentModel.DataAnnotations;

namespace InterviewPass.WebApi.Models.Question
{
    public class TrueFalseQuestionModel
    {
        [Required]
        public bool? IsTrue { get; set; }
    }
}
