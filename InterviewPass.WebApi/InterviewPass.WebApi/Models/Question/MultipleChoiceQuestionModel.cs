using System.ComponentModel.DataAnnotations;

namespace InterviewPass.WebApi.Models.Question
{
    public class MultipleChoiceQuestionModel : QuestionModel
    {
        [Required]
        public bool? HasSignleChoice { get; set; }
        [Required]
        public List<PossibilityModel> Possibilities { get; set; }
    }
}
