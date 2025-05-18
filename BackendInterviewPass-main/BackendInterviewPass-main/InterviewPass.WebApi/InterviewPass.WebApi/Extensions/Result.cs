using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterviewPass.WebApi.Extensions
{
    [Route("api/[controller]")]
    [ApiController]
    public class Result : ControllerBase
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int CandidateId { get; set; }
        public double Score { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
