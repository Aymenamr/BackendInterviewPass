using InterviewPass.WebApi.Models;
using InterviewPass.DataAccess.UnitOfWork;
using System.Linq;
using InterviewPass.DataAccess.Services;

public class AnswerScoringService : IAnswerScoringService
{
    private readonly IUnitOfWork _unitOfWork;

    public AnswerScoringService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public double CalculateScore(AnswerModel answerModel)
    {
        if (answerModel.Type == "TrueFalse")
        {
            return answerModel.SelectedTrueFalse == answerModel.IsTrueSelected ? 100.0 : 0.0;
        }
        else if (answerModel.Type == "MultipleChoice")
        {
            //Get all correct choices
            var correctChoices = _unitOfWork.PossibilityRepo
                .GetAll()
                .Where(p => p.QuestionId == answerModel.QuestionId && p.IsTheCorrectAnswer.GetValueOrDefault(false))
                .Select(p => p.Id)
                .ToList();

            if (!correctChoices.Any())
                return 0.0;
            //compare between user choices and correct choices
            int totalCorrect = correctChoices.Count;
            int selectedCorrect = answerModel.SelectedChoiceIds.Intersect(correctChoices).Count();

            return (double)selectedCorrect / totalCorrect * 100;
        }
        else if (answerModel.Type == "Objective" || answerModel.Type == "Practical")
        {
            //manual score
            return answerModel.ManualScore ?? 0.0;
        }

        return 0.0;
    }
}
