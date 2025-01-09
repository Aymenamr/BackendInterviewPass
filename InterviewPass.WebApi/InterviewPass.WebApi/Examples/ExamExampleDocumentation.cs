using System.Numerics;
using System.Xml.Linq;
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
                    Name = "C# exam",
                    Description = "test LINQ",
                    Author = "Hr Name",
                    MinScore = 40,
                    MaxScore = 80,
                    DeadLineInNbrOfDays = 20,
                    NbrOfQuestion = 15,
                    Questions = new List<QuestionModel> {
                        new MultipleChoiceQuestionModel { 
                            Score=10,Content = "which is a C# framework",
                            HasSignleChoice=true, 
                            Possibilities = new List<PossibilityModel> {
                                            new PossibilityModel { 
                                                IsTheCorrectAnswer = true, 
                                                Content = "Asp.net"
                                            },
                                            new PossibilityModel { 
                                                IsTheCorrectAnswer = false, 
                                                Content = "Flask"
                                            } } },
                        new TrueFalseQuestionModel{
                            Content="questions content",
                            IsTrue=false,Score=5
                            },
                        new PracticalQuestionModel
                        {
                            Content="Practical Question content",
                            Score=20
                        },
                        new ObjectiveQuestionModel
                        { 
                            Content="Objective Question content",
                            Score=20
                        } }

                });
        }
    }
}
