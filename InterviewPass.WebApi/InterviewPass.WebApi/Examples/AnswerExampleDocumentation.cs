using InterviewPass.WebApi.Models;
using Swashbuckle.AspNetCore.Filters;

public class AnswerExampleDocumentation : IExamplesProvider<AnswerModel>
{
    public AnswerModel GetExamples()
    {
        return new AnswerModel
        {
            ExamId = "5439dcc2-cefb-4f25-bf53-52b64646a43",
            QuestionId = "84e4d58b-0ff5-4e03-a205-2f9267d46356",
            UserId = "user-9876",
            ResultId= "result-112233",
            Type = "TrueFalse",
            SelectedTrueFalse = true,
            TextAnswer = "",
            GitHubLink = null,
            ImageAnswer = null,
            ZipAnswer = null,
            ManualScore = 100,
            SelectedChoiceIds = new List<string>()
        };
    }
}