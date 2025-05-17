namespace InterviewPass.DataAccess.Entities
{
	public partial class JobBenefit
	{
		public string Id { get; set; } = null!;
		public string? JobId { get; set; }

		public virtual Job? Job { get; set; }
		public string? BenefitId { get; set; }
		public virtual Benefits? Benefit { get; set; }
	}
}
