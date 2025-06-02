using InterviewPass.WebApi.Models.Question;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

namespace InterviewPass.WebApi.Examples
{
    public class QuestionExampleDocumentation : IMultipleExamplesProvider<QuestionModel>
    {
        public IEnumerable<SwaggerExample<QuestionModel>> GetExamples()
        {
            yield return SwaggerExample.Create(
                "General Question Example",
                new QuestionModel
                {
                    Content = "What is polymorphism in object-oriented programming?",
                    SkillId = "1452",
                    Score = 5,
                    Id = "b1234567-89ab-4def-90cd-1234567890ab" 
                });
        }
    }
}
