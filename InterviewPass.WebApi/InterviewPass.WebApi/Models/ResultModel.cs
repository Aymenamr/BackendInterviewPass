namespace InterviewPass.WebApi.Models
{
    public class ResultModel
    {
        public string Id { get; set; } = null!;

        public string? Status { get; set; }

        public string? ExamId { get; set; }

        public double? Score { get; set; }

        public string? UserId { get; set; }

        public DateTime? DeadLine { get; set; }

        public DateTime StartDate { get; set; }
    }
}
