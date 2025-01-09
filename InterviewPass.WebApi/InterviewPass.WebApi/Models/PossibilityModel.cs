using System.ComponentModel.DataAnnotations;

namespace InterviewPass.WebApi.Models
{
    public class PossibilityModel
    {
        public string? Id { get; set; } = null!;

        public string? QuestionId { get; set; }

        public bool? IsTheCorrectAnswer { get; set; }

        public string? Content { get; set; }

    }
}
