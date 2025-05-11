using System.ComponentModel.DataAnnotations;

namespace InterviewPass.WebApi.Models
{
	public class JobBenefitModel
	{

		public string? Id { get; set; }

		public string? JobId { get; set; }

		[Required(ErrorMessage = "مطلوب تحديد الميزة")]
		public string BenefitId { get; set; }
	}
}
