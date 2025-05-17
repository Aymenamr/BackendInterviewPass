using InterviewPass.WebApi.Models;

namespace InterviewPass.WebApi.Processors
{
    public interface IAnswerProcessor
    {
        AnswerModel ProcessAnswer(AnswerModel answer);
    }
}
