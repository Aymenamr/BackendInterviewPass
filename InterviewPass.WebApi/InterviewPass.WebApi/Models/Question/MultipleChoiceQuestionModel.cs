using System.ComponentModel.DataAnnotations;

namespace InterviewPass.WebApi.Models.Question
{
    public class MultipleChoiceQuestionModel
    {
        [Required]
        public bool? HasSignleChoice { get; set; }
    }
}
