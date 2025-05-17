using InterviewPass.WebApi.Controllers;
using InterviewPass.WebApi.Models;

namespace InterviewPass.WebApi.Processors
{
    public class AnswerProcessor : IAnswerProcessor
    {
        private readonly ILogger<AnswerController> _logger;
        public AnswerProcessor(ILogger<AnswerController> logger)
        {
            _logger = logger;
        }

        public AnswerModel ProcessAnswer(AnswerModel answer)
        {
            _logger.LogInformation("Processing answer");
            return answer;
        }
    }
}
