using InterviewPass.DataAccess.Entities.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace InterviewPass.DataAccess.Entities;

public partial class InterviewPassContext : DbContext
{
	public string DbPath { get { return _configuration["DbPath"]; } }
	private IConfiguration _configuration;
	public InterviewPassContext(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public InterviewPassContext(DbContextOptions<InterviewPassContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Answer> Answers { get; set; }

	public virtual DbSet<Exam> Exams { get; set; }

	public virtual DbSet<Field> Fields { get; set; }

	public virtual DbSet<Possibility> Possibilities { get; set; }

	public virtual DbSet<Question> Questions { get; set; }

	public virtual DbSet<QuestionExam> QuestionExams { get; set; }

	public virtual DbSet<SelectedPossibility> SelectedPossibilities { get; set; }

	public virtual DbSet<Skill> Skills { get; set; }

	public virtual DbSet<SkillBySeeker> SkillBySeekers { get; set; }

	public virtual DbSet<UserHr> UserHrs { get; set; }

	public virtual DbSet<UserJobSeeker> UserJobSeekers { get; set; }
	public virtual DbSet<Job> Jobs { get; set; }
	public virtual DbSet<Benefits> Benefits { get; set; }
	public virtual DbSet<EmploymentType> EmploymentTypes { get; set; }
	public virtual DbSet<JobBenefit> JobBenefits { get; set; }

	public virtual DbSet<JobFile> JobFiles { get; set; }

	public virtual DbSet<JobSkill> JobSkills { get; set; }
    public virtual DbSet<Result> Results { get; set; }




    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{ 
		 optionsBuilder.UseSqlite(string.Format("DataSource={0}", DbPath));
		// تفعيل المفاتيح الأجنبية
		optionsBuilder.LogTo(Console.WriteLine); // اختيارية، لمراقبة الاستعلامات

		base.OnConfiguring(optionsBuilder);
	}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Answer>(entity =>
		{
			entity.ToTable("Answer");

			entity.Property(e => e.Id).HasColumnType("STRING");
			entity.Property(e => e.ExamId).HasColumnType("STRING");
			entity.Property(e => e.GitHubLink).HasColumnType("STRING");
			entity.Property(e => e.IsTrueSelected).HasColumnType("BOOLEAN");
			entity.Property(e => e.ObtainedScore).HasColumnType("DOUBLE");
			entity.Property(e => e.QuestionId).HasColumnType("STRING");
			entity.Property(e => e.ResultId).HasColumnType("STRING");
			entity.Property(e => e.TextAnswer).HasColumnType("STRING");
			entity.Property(e => e.Type).HasColumnType("STRING");
			entity.Property(e => e.UserId).HasColumnType("STRING");

			entity.HasOne(d => d.Exam).WithMany(p => p.Answers).HasForeignKey(d => d.ExamId);

			entity.HasOne(d => d.Question).WithMany(p => p.Answers).HasForeignKey(d => d.QuestionId);

			entity.HasOne(d => d.Result).WithMany(p => p.Answers).HasForeignKey(d => d.ResultId);

			entity.HasOne(d => d.User).WithMany(p => p.Answers).HasForeignKey(d => d.UserId);
		});

		modelBuilder.Entity<Exam>(entity =>
		{
			entity.ToTable("Exam");

			entity.HasIndex(e => e.Name, "IX_Exam_Name").IsUnique();
			entity.Property(e => e.Id).HasColumnType("STRING");
			entity.Property(e => e.CreatedBy).HasColumnType("STRING");
			entity.Property(e => e.CreationDate).HasColumnType("DATETIME");

			entity.Property(e => e.Description).HasColumnType("STRING");
			entity.Property(e => e.MaxScore).HasColumnType("DOUBLE");
			entity.Property(e => e.MinScore).HasColumnType("DOUBLE");
			entity.Property(e => e.Name).HasColumnType("VARCHAR");

			entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Exams).HasForeignKey(d => d.CreatedBy);
		});

		modelBuilder.Entity<Field>(entity =>
		{
			entity.ToTable("Field");

			entity.HasIndex(e => e.Name, "IX_Field_Name").IsUnique();

			entity.Property(e => e.Id).HasColumnType("STRING");
			entity.Property(e => e.Name).HasColumnType("STRING");
		});

		modelBuilder.Entity<Possibility>(entity =>
		{
			entity.Property(e => e.Id).HasColumnType("STRING");
			entity.Property(e => e.Content).HasColumnType("STRING");
			entity.Property(e => e.IsTheCorrectAnswer).HasColumnType("BOOLEAN");
			entity.Property(e => e.QuestionId).HasColumnType("STRING");

			entity.HasOne(d => d.Question).WithMany(p => p.Possibilities).HasForeignKey(d => d.QuestionId);
		});

		modelBuilder.Entity<Question>(entity =>
		{
			entity.ToTable("Question").HasDiscriminator<string>("Type")
									   .HasValue<MultipleChoiceQuestion>("MultipleChoice")
									   .HasValue<TrueFalseQuestion>("TrueFalse")
									   .HasValue<ObjectiveQuestion>("Objective")
									   .HasValue<PracticalQuestion>("Practical");

			entity.Property(e => e.Id).HasColumnType("STRING");
			entity.Property(e => e.Content).HasColumnType("STRING");
			entity.Property(e => e.Score).HasColumnType("DOUBLE");
			entity.Property(e => e.Type).HasColumnType("STRING");
			entity.HasOne(d => d.Skill).WithMany(p => p.Questions).HasForeignKey(d => d.SkillId);

		});

		modelBuilder.Entity<MultipleChoiceQuestion>()
				.Property(e => e.HasSignleChoice).HasColumnType("BOOLEAN");

		modelBuilder.Entity<TrueFalseQuestion>()
				.Property(e => e.IsTrue).HasColumnType("BOOLEAN");
		modelBuilder.Entity<QuestionExam>(entity =>
		{
			entity.Property(e => e.IdExam).HasColumnType("STRING");
			entity.Property(e => e.IdQuestion).HasColumnType("STRING");

			entity.HasOne(d => d.IdExamNavigation).WithMany(p => p.QuestionExams)
				.HasForeignKey(d => d.IdExam)
				.OnDelete(DeleteBehavior.ClientSetNull);

			entity.HasOne(d => d.IdQuestionNavigation).WithMany(p => p.QuestionExams)
				.HasForeignKey(d => d.IdQuestion)
				.OnDelete(DeleteBehavior.ClientSetNull);
		});

		modelBuilder.Entity<Result>(entity =>
		{
			entity.Property(e => e.Id).HasColumnType("STRING");
			entity.Property(e => e.CandidateSucceeded).HasColumnType("BOOLEAN");
			entity.Property(e => e.DeadLine).HasColumnType("DATETIME");
			entity.Property(e => e.ExamId).HasColumnType("STRING");
			entity.Property(e => e.Score).HasColumnType("DOUBLE");
			entity.Property(e => e.Status).HasColumnType("STRING");
			entity.Property(e => e.UserId).HasColumnType("STRING");

			entity.HasOne(d => d.Exam).WithMany(p => p.Results).HasForeignKey(d => d.ExamId);

			entity.HasOne(d => d.User).WithMany(p => p.Results).HasForeignKey(d => d.UserId);
		});

		modelBuilder.Entity<SelectedPossibility>(entity =>
		{
			entity.Property(e => e.IdAnswer).HasColumnType("STRING");
			entity.Property(e => e.IdPossibility).HasColumnType("STRING");

			entity.HasOne(d => d.IdAnswerNavigation).WithMany(p => p.SelectedPossibilities).HasForeignKey(d => d.IdAnswer);

			entity.HasOne(d => d.IdPossibilityNavigation).WithMany(p => p.SelectedPossibilities).HasForeignKey(d => d.IdPossibility);
		});

		modelBuilder.Entity<Skill>(entity =>
		{
			entity.ToTable("Skill");

			entity.HasIndex(e => e.Name, "IX_Skill_Name").IsUnique();

			entity.Property(e => e.Id).HasColumnType("VARCHAR");
			entity.Property(e => e.FieldId).HasColumnType("STRING");
			entity.Property(e => e.Name).HasColumnType("STRING");

			entity.HasOne(d => d.Field).WithMany(p => p.Skills).HasForeignKey(d => d.FieldId);
		});

		modelBuilder.Entity<SkillBySeeker>(entity =>
		{
			entity.ToTable("SkillBySeeker");

			entity.Property(e => e.JobSeekerId).HasColumnType("STRING");
			entity.Property(e => e.SkillId).HasColumnType("STRING");

			entity.HasOne(d => d.JobSeeker).WithMany(p => p.SkillBySeekers).HasForeignKey(d => d.JobSeekerId);

			entity.HasOne(d => d.Skill).WithMany(p => p.SkillBySeekers).HasForeignKey(d => d.SkillId);
		});

		modelBuilder.Entity<UserHr>(entity =>
		{
			entity.ToTable("UserHr");

			entity.HasIndex(e => e.Login, "IX_UserHr_Login").IsUnique();

			entity.Property(e => e.Id).HasColumnType("STRING");
			entity.Property(e => e.Company).HasColumnType("STRING");
			entity.Property(e => e.DateOfBirth).HasColumnType("DATETIME");
			entity.Property(e => e.Email).HasColumnType("STRING");
			entity.Property(e => e.Login).HasColumnType("STRING");
			entity.Property(e => e.Name).HasColumnType("STRING");
			entity.Property(e => e.PasswordHash).HasColumnType("STRING");
			entity.Property(e => e.Phone).HasColumnType("STRING");
		});

		modelBuilder.Entity<UserJobSeeker>(entity =>
		{
			entity.ToTable("UserJobSeeker");

			entity.HasIndex(e => e.Name, "IX_UserJobSeeker_Name").IsUnique();

			entity.Property(e => e.Id).HasColumnType("STRING");
			entity.Property(e => e.DateOfBirth).HasColumnType("DATETIME");
			entity.Property(e => e.Email).HasColumnType("STRING");
			entity.Property(e => e.Login).HasColumnType("STRING");
			entity.Property(e => e.Name).HasColumnType("STRING");
			entity.Property(e => e.PasswordHash).HasColumnType("VARCHAR");
			entity.Property(e => e.Phone).HasColumnType("VARCHAR");
		});

		modelBuilder.Entity<Job>(entity =>
		{
			entity.ToTable("Job");

			entity.HasIndex(e => e.Title, "IX_Job_Title");

			entity.Property(e => e.Id).HasColumnType("STRING");
			entity.Property(e => e.Title).HasColumnType("STRING");
			entity.Property(e => e.ShortDescription).HasColumnType("STRING");
			entity.Property(e => e.Image).HasColumnType("varbinary(max)");
			entity.Property(e => e.Experience).HasColumnType("INTEGER");
			entity.Property(e => e.WorkingSchedule).HasColumnType("STRING");
			entity.Property(e => e.Role).HasColumnType("STRING");
			entity.Property(e => e.Salary).HasColumnType("DOUBLE");

			entity.Property(e => e.EmploymentTypeId).HasColumnType("STRING");

			entity.HasOne(e => e.EmploymentType)
				  .WithMany(e => e.Jobs)
				  .HasForeignKey(e => e.EmploymentTypeId)
				  .OnDelete(DeleteBehavior.Restrict);
		});

		modelBuilder.Entity<EmploymentType>(entity =>
		{
			entity.ToTable("EmploymentType");

			entity.Property(e => e.Id).HasColumnType("STRING");
			entity.Property(e => e.Type).HasColumnType("STRING");

		});

		modelBuilder.Entity<Benefits>(entity =>
		{
			entity.ToTable("Benefits");

			entity.Property(e => e.Id).HasColumnType("STRING").IsRequired();
			entity.Property(e => e.Name).HasColumnType("STRING");

		});

		modelBuilder.Entity<JobBenefit>(entity =>
		{
			entity.ToTable("JobBenefit");

			entity.Property(e => e.Id).HasColumnType("STRING").IsRequired();
			entity.Property(e => e.JobId).HasColumnType("STRING").IsRequired();
			entity.Property(e => e.BenefitId).HasColumnType("STRING").IsRequired();

			entity.HasOne(e => e.Job).WithMany(d => d.JobBenefits).HasForeignKey(d => d.JobId)
			.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(d => d.Benefit).WithMany(d => d.JobBenefits).HasForeignKey(d => d.BenefitId)
			.OnDelete(DeleteBehavior.Restrict);


		});

		modelBuilder.Entity<JobSkill>(entity =>
		{
			entity.ToTable("JobSkill");

			entity.Property(e => e.Id).HasColumnType("STRING");

			entity.Property(e => e.JobId).HasColumnType("STRING");
			entity.Property(e => e.SkillId).HasColumnType("STRING");

			entity.HasOne(d => d.Job)
				  .WithMany(d => d.JobSkills)
				  .HasForeignKey(d => d.JobId)
				  .OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(d => d.Skill)
				  .WithMany(d => d.JobSkills)
				  .HasForeignKey(d => d.SkillId)
				  .OnDelete(DeleteBehavior.Restrict);
		});

		modelBuilder.Entity<JobFile>(entity =>
		{
			entity.ToTable("JobFile");

			entity.Property(e => e.Id).HasColumnType("STRING").IsRequired();
			entity.Property(e => e.File).HasColumnType("varbinary(max)");
			entity.Property(e => e.FileName).HasColumnType("STRING").IsRequired(false);
			entity.Property(e => e.JobId).HasColumnType("STRING").IsRequired(false);

			entity.HasOne(d => d.Job).WithMany(j => j.JobFiles).HasForeignKey(d => d.JobId)
			.OnDelete(DeleteBehavior.Restrict);
		});
		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
