using InterviewPass.DataAccess.Entities;
using InterviewPass.WebApi.Models;

namespace InterviewPass.DataAccess.Services
{
    public interface IAnswerScoringService
    {
        double CalculateScore(AnswerModel answerModel);
    }
}
