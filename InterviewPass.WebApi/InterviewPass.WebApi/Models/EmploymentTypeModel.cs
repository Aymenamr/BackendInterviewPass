using System.ComponentModel.DataAnnotations;

namespace InterviewPass.WebApi.Models
{
	public class EmploymentTypeModel
	{
		public string Id { get; set; }

		[StringLength(50, ErrorMessage = "Employment Type can't be longer than 50 characters.")]
		public string? Type { get; set; }

		public List<JobModel> Jobs { get; set; } = new List<JobModel>();
	}
}
