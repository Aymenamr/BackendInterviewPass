namespace InterviewPass.DataAccess.Entities
{
	public partial class EmploymentType
	{
		public string Id { get; set; } = null!;
		public string? Type { get; set; }

		public string? JobId { get; set; }
		public virtual Job? Job { get; set; }
	}
}
