namespace InterviewPass.DataAccess.Entities
{
	public partial class Job
	{
		public string Id { get; set; } = null!;
		public string? Title { get; set; }
		public string? ShortDescription { get; set; }
		public byte[]? Image { get; set; }
		public int? Experience { get; set; }
		public string? WorkingSchedule { get; set; }
		public string? Role { get; set; }
		public double? Salary { get; set; }
		public string? EmploymentTypeId { get; set; }
		public virtual EmploymentType? EmploymentType { get; set; }
		public virtual ICollection<JobFile> JobFiles { get; set; } = new List<JobFile>();
		public virtual ICollection<JobBenefit> JobBenefits { get; set; } = new List<JobBenefit>();
		public virtual ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();


	}
}
