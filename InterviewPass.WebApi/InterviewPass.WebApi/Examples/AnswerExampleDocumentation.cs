using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Models.Question;
using InterviewPass.WebApi.Models.QuestionAnswer;
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
                    Type = "MultipleChoiceQuestionAnswer",
                    MinScore = 40,
                    MaxScore = 80,
                    NbrOfAnswer = 15,
                    Answers = new List<QuestionAnswerModel>{
                        new PracticalQuestionAnswerModel{
                            Id = null,
                            Content="Practical Question content",
                            Score=20
                        },
                        new TrueFalseQuestionAnswerModel{
                            Id = null,
                            Score = 5,
                            Content = new bool {
                                IsTrue = true
                            }
                        },
                        new MultipleChoiceQuestionAnswerModel{
                            Id = null,
                            Score=10,
                            AnswerContent = new PossibilityModel{
                                IsTheCorrectAnswer = true,
                                Content = "Asp.net"
                            }
                        },
                        new ObjectiveQuestionAnswerModel{
                            Id = null,
                            Score=20,
                            Content="Objective Answer content"
                        }
                    }
                }
            );
        }
    }
}
