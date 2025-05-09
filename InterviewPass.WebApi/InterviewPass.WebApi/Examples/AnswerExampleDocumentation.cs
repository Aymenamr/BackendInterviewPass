using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Models.Question;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

namespace InterviewPass.WebApi.Examples
{
    public class AnswerExampleDocumentation
    {
        public IEnumerable<SwaggerExample<AnswerModel>> GetExamples() 
        {
            yield return SwaggerExample.Create(
                "Answer Example",
                new AnswerModel()
                {
                    Id = null,
                    Name = "C# exam answer",
                    MinScore = 40,
                    MaxScore = 80,
                    NbrOfAnswer = 15,
                    Answers = new List<AnswerModel>
                    {
                    }
                }
                );
        }
    }
}
