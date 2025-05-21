using System.ComponentModel.DataAnnotations;

namespace InterviewPass.WebApi.Models
{
	public class JobModel
	{
		public string? Id { get; set; }

		[StringLength(100, ErrorMessage = "Title can't be longer than 100 characters.")]
		public string? Title { get; set; }

		[StringLength(500, ErrorMessage = "Short Description can't be longer than 500 characters.")]
		public string? ShortDescription { get; set; }
		public string? Image { get; set; }

		[Range(1, 30, ErrorMessage = "Experience must be a positive number.")]
		public int? Experience { get; set; }

		[StringLength(100, ErrorMessage = "Working Schedule can't be longer than 100 characters.")]
		public string? WorkingSchedule { get; set; }

		[StringLength(100, ErrorMessage = "Role can't be longer than 100 characters.")]
		public string? Role { get; set; }

		[Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
		public double? Salary { get; set; }
		public string? EmploymentTypeId { get; set; }
		public List<string> Skills { get; set; } = new List<string>();
		public List<string> Benefits { get; set; } = new List<string>();
		public List<string> Files { get; set; } = new List<string>();


	}
}
