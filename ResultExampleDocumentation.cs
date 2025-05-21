using System.Numerics;
using System.Xml.Linq;
using InterviewPass.DataAccess.Entities;
using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Models.Question;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

namespace InterviewPass.WebApi.Examples
{
    public class ResultExampleDocumentation : IMultipleExamplesProvider<ResultModel>
    {
        public IEnumerable<SwaggerExample<ResultModel>> GetExamples()
        {
            yield return SwaggerExample.Create(
                "Exam Example",
                new ResultModel()
                {
                    CandidateId = null,
                    ExamId = null,
                    Score = 50,
                    SubmittedAt = DateTime.UtcNow, 
                    Questions = new List<QuestionModel> {
                        new MultipleChoiceQuestionModel {
                            Id = null,
                            Score=2,
                            Content = "which is a C# framework",
                            HasSignleChoice=true,
                            SkillId= Guid.NewGuid().ToString(),
                            Possibilities = new List<PossibilityModel> {
                                            new PossibilityModel {
                                                Id = null,
                                                IsTheCorrectAnswer = true,
                                                Content = "Asp.net"
                                            },
                                            new PossibilityModel {
                                                Id = null,
                                                IsTheCorrectAnswer = false,
                                                Content = "Flask"
                                            } } },
                        new TrueFalseQuestionModel{
                            Id = null,
                            Content="questions content",
                            IsTrue=false,
                            SkillId= Guid.NewGuid().ToString(),
                            Score=5

                            }
                    }
                });
        }
    }
}
