namespace InterviewPass.DataAccess.Entities
{
	public partial class JobSkill
	{
		public string Id { get; set; } = null!;

		public string? JobId { get; set; }
		public virtual Job? Job { get; set; }

		public string? SkillId { get; set; }
		public virtual Skill? Skill { get; set; }
	}
}
