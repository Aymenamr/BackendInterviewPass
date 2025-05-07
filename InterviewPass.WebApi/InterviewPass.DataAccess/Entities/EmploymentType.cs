namespace InterviewPass.DataAccess.Entities
{
	public partial class EmploymentType
	{
		public string Id { get; set; } = null!;
		public string? Type { get; set; }
		public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

	}
}
