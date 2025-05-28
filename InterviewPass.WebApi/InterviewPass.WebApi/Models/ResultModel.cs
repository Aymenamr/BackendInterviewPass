using InterviewPass.WebApi.Models.Question;

namespace InterviewPass.WebApi.Models
{
    public class ResultModel
    {
        public string? Status { get; set; }

        public string? ExamId { get; set; }

        public double? Score { get; set; }

        public string? UserId { get; set; }

        public DateTime? DeadLine { get; set; }

        public DateTime StartDate { get; set; }


        public bool? CandidateSucceeded { get; set; }
    }
}
