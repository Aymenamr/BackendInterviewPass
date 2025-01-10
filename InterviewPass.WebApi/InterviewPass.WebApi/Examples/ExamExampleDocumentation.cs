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
    public class ExamExampleDocumentation : IMultipleExamplesProvider<ExamModel>
    {
        public IEnumerable<SwaggerExample<ExamModel>> GetExamples()
        {
            yield return SwaggerExample.Create(
                "Exam Example",
                new ExamModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "C# exam",
                    Description = "test LINQ",
                    Author = "Hr Name",
                    MinScore = 40,
                    MaxScore = 80,
                    DeadLineInNbrOfDays = 20,
                    NbrOfQuestion = 15,
                    Questions = new List<QuestionModel> {
                        new MultipleChoiceQuestionModel {
                            Id = Guid.NewGuid().ToString(),
                            Score=10,
                            Content = "which is a C# framework",
                            HasSignleChoice=true,
                            SkillId= Guid.NewGuid().ToString(),
                            Possibilities = new List<PossibilityModel> {
                                            new PossibilityModel {
                                                Id = Guid.NewGuid().ToString(),
                                                IsTheCorrectAnswer = true,
                                                Content = "Asp.net"
                                            },
                                            new PossibilityModel {
                                                Id = Guid.NewGuid().ToString(),
                                                IsTheCorrectAnswer = false,
                                                Content = "Flask"
                                            } } },
                        new TrueFalseQuestionModel{
                            Id = Guid.NewGuid().ToString(),
                            Content="questions content",
                            IsTrue=false,
                            SkillId= Guid.NewGuid().ToString(),
                            Score=5

                            },
                        new PracticalQuestionModel
                        {
                            Id = Guid.NewGuid().ToString(),
                            SkillId= Guid.NewGuid().ToString(),
                            Content="Practical Question content",
                            Score=20
                        },
                        new ObjectiveQuestionModel
                        {
                            Id = Guid.NewGuid().ToString(),
                            SkillId= Guid.NewGuid().ToString(),
                            Content="Objective Question content",
                            Score=20
                        } }

                });
        }
    }
}
