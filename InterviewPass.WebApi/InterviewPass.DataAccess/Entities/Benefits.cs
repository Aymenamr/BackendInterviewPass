namespace InterviewPass.DataAccess.Entities
{
	public partial class Benefits
	{
		public string Id { get; set; } = null!;
		public string? Name { get; set; }

		public virtual ICollection<JobBenefit> JobBenefits { get; set; } = new List<JobBenefit>();

	}
}
