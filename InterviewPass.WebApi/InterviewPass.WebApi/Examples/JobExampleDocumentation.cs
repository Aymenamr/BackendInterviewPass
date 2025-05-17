using InterviewPass.WebApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace InterviewPass.WebApi.Examples
{
	public class JobExampleDocumentation : IMultipleExamplesProvider<JobModel>
	{
		public IEnumerable<SwaggerExample<JobModel>> GetExamples()
		{
			yield return SwaggerExample.Create(
					   "Example Job",
					   new JobModel
					   {
						   Id = null,
						   Title = "Senior Backend Developer",
						   ShortDescription = "Develop and maintain APIs for the platform.",
						   ImagePath = "images/backend-dev.png",
						   Experience = 5,
						   WorkingSchedule = "Full-time",
						   Role = "Team Lead",
						   Salary = 8000,
						   EmploymentTypeId = "1",

						   Skills = new List<string>
						   {
									"Csharp",
									"WebApi"
						   },

						   Benefits = new List<string>
						   {
									"Car"
						   },

						   Files = new List<string>
						   {
								  Convert.ToBase64String(Encoding.UTF8.GetBytes("JobSpecs.pdf")),
								  Convert.ToBase64String(Encoding.UTF8.GetBytes("ContractTemplate.pdf"))
						   }
					   });
		}
	}
}
