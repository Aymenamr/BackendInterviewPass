using System.ComponentModel.DataAnnotations;

namespace InterviewPass.WebApi.Models
{
    public class PossibilityModel
    {
        public string? Id { get; set; } = null!;

        public string? QuestionId { get; set; }
        [Required]
        public bool? IsTheCorrectAnswer { get; set; }
        [Required]
        [MaxLength(length: 10000, ErrorMessage = "Possibility content description is too large.")]
        public string? Content { get; set; }

    }
}
