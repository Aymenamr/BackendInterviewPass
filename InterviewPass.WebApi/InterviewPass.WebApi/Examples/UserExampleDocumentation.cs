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
                    DateOfBirth = DateTime.Parse("22-10-1965"),
                     Email="test@gmail.com",
                     LevelOfExperience=10,
                     Login="test 1",
                     Name="first name",
                     PasswordHash= "3cdbe355f60f1b4d04496cfe9879ab62851b58ab",
                     UserType="JobSeeker"

                });
        }
    }
}
