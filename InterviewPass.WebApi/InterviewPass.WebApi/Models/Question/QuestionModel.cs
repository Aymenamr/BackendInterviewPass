using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;
using JsonSubTypes;


namespace InterviewPass.WebApi.Models.Question
{
    


    public class QuestionModel
    {
        public string? Id { get; set; } = null!;
        [Required]
        public string? Content { get; set; }
        [Required]
        public double? Score { get; set; }
        [Required]
        [RegularExpression(@"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$", ErrorMessage = "Skill ID must be GUID.")]
        public string? SkillId { get; set; }

        public string QuestionType { get; set; } 


    }
}
