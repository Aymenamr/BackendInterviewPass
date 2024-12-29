using InterviewPass.DataAccess.Entities;
using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Models.User;
using Swashbuckle.AspNetCore.Filters;

namespace InterviewPass.WebApi.Examples
{
    public class UserExampleDocumentation : IMultipleExamplesProvider<UserJobSeekerModel>
    {
        public IEnumerable<SwaggerExample<UserJobSeekerModel>> GetExamples()
        {
            yield return SwaggerExample.Create(
                "JobSeeker Example",
                new UserJobSeekerModel()
                {
                    DateOfBirth = DateTime.ParseExact("22-10-1965", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),            Email = "test@gmail.com",
                     LevelOfExperience=10,
                     Login="test1",
                     Name="first name",
                     PasswordHash= "3cdbe355f60f1b4d04496cfe9879ab62851b58ab",
                     UserType="JobSeeker",
                     Phone="12344",
                     Skills=new List<SkillModel> { new SkillModel { Id = "8e3c91b1-8e64-4f25-8f1b-8b7dde99cbf4" } }

                });
        }
    }
}
