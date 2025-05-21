using System.ComponentModel.DataAnnotations;
using InterviewPass.WebApi.Models.Question;

namespace InterviewPass.WebApi.Models
{
    public class ResultModel
    {
        public string Id { get; set; }
        public int? CandidateId { get; set; }
        public int? ExamId { get; set; }
        public double Score { get; set; }
        public DateTime SubmittedAt { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }
}
