namespace InterviewPass.DataAccess.Entities
{
	public partial class Job
	{
		public string Id { get; set; } = null!;
		public string? Title { get; set; }
		public string? ShortDescription { get; set; }
		public string? ImagePath { get; set; }
		public int? experience { get; set; }
		public string? WorkingSchedule { get; set; }
		public string? Role { get; set; }
		public double? Salary { get; set; }
		public virtual ICollection<EmploymentType> EmploymentTypes { get; set; } = new List<EmploymentType>();
		public virtual ICollection<JobFile> JobFiles { get; set; } = new List<JobFile>();
		public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
		public virtual ICollection<JobBenefit> JobBenefits { get; set; } = new List<JobBenefit>();


	}
}
