
namespace InterviewPass.WebApi.Models
{
    public interface IResultModel
    {
        bool? CandidateSucceeded { get; set; }
        DateTime? DeadLine { get; set; }
        string? ExamId { get; set; }
        string? Id { get; set; }
        string Name { get; set; }
        double? Score { get; set; }
        DateTime StartDate { get; set; }
        string? Status { get; set; }
        string? UserId { get; set; }
    }
}