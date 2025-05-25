using InterviewPass.DataAccess.Entities;
using InterviewPass.WebApi.Models.User;

namespace InterviewPass.WebApi.Models
{
    public class AnswerModel
    {
        public string? ExamId { get; set; }
        public string? QuestionId { get; set; }
        public string? Type { get; set; }
        public string? UserId { get; set; }
        public bool? IsTrueSelected { get; set; }
        public string? TextAnswer { get; set; }
        public string? ImageAnswer { get; set; }
        public string? ZipAnswer { get; set; }
        public string? GitHubLink { get; set; }
        public string? ResultId { get; set; }
        public List<PossibilityModel> SelectedPossibilities { get; set; } = new List<PossibilityModel>();
    }
}
