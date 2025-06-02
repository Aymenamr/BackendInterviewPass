namespace InterviewPass.DataAccess.Entities
{
	public partial class JobFile
	{
		public string Id { get; set; } = null!;
		public byte[]? File { get; set; }
		public string? FileName { get; set; }

		public string? JobId { get; set; }

		public virtual Job? Job { get; set; }

	}
}
