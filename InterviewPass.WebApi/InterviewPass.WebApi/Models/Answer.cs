using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace InterviewPass.WebApi.Models
{
    public class AnswerModel
    {
        public string? Id { get; set; }

        [Required]
        public string ExamId { get; set; } = null!;

        [Required]
        public string QuestionId { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;  // "TrueFalse", "MultipleChoice", etc.

        public bool SelectedTrueFalse { get; set; }

        public string? TextAnswer { get; set; }

        public byte[]? ImageAnswer { get; set; }

        public byte[]? ZipAnswer { get; set; }

        public string? GitHubLink { get; set; }

        public string? ResultId { get; set; }

        public List<string> SelectedChoiceIds { get; set; } = new List<string>();

        public double? ManualScore { get; set; }

        public double? ObtainedScore { get; set; }
        public bool IsTrueSelected { get; internal set; }
    }
}
