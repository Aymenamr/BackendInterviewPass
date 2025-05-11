using System.ComponentModel.DataAnnotations;

namespace InterviewPass.WebApi.Models
{
	public class BenefitsModel
	{
		public string Id { get; set; }

		[StringLength(100, ErrorMessage = "Benefit Name can't be longer than 100 characters.")]
		public string? Name { get; set; }

		public List<JobBenefitModel> JobBenefits { get; set; } = new List<JobBenefitModel>();
	}
}
