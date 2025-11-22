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
					DateOfBirth = DateTime.ParseExact("22-10-1965", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
					Email = "test@gmail.com",
					LevelOfExperience = 10,
					Login = "test1",
					Name = "first name",
					PasswordHash = "4d22034669e5037b419a04069b4b99a607b4a707990882690492514c7d6e7bb5",
					Phone = "12344",
					Skills = new List<SkillModel> { new SkillModel { Id = "1" } }

				});
		}
	}
}
