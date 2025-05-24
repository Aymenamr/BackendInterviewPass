using InterviewPass.WebApi.Models.Question;

namespace InterviewPass.WebApi.Models
{
    public class ResultModel
    {
        public int CandidateId { get; set; }
        public int ExamId { get; set; }
        public double Score { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
