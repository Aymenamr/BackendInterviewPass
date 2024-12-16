﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InterviewPass.DataAccess.Entities;

public partial class InterviewPassContext : DbContext
{
    public InterviewPassContext()
    {
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

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<SelectedPossibility> SelectedPossibilities { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<SkillBySeeker> SkillBySeekers { get; set; }

    public virtual DbSet<UserHr> UserHrs { get; set; }

    public virtual DbSet<UserJobSeeker> UserJobSeekers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("DataSource=C:\\aymen\\Mentorship\\CapableGraduate\\InterviewPass.WebApi-Session3\\InterviewPass.DataAccess\\Database\\interviewPass.db");

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

            entity.Property(e => e.Id).HasColumnType("STRING");
            entity.Property(e => e.CreatedBy).HasColumnType("STRING");
            entity.Property(e => e.Description).HasColumnType("STRING");
            entity.Property(e => e.MaxScore).HasColumnType("DOUBLE");
            entity.Property(e => e.MinScore).HasColumnType("DOUBLE");
            entity.Property(e => e.Name).HasColumnType("VARCHAR");
            entity.Property(e => e.StartingDate).HasColumnType("DATETIME");

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
            entity.ToTable("Question");

            entity.Property(e => e.Id).HasColumnType("STRING");
            entity.Property(e => e.Content).HasColumnType("STRING");
            entity.Property(e => e.HasSignleChoice).HasColumnType("BOOLEAN");
            entity.Property(e => e.IsTrue).HasColumnType("BOOLEAN");
            entity.Property(e => e.Score).HasColumnType("DOUBLE");
            entity.Property(e => e.Type).HasColumnType("STRING");
        });

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

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("STRING");
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

            entity.HasIndex(e => e.Name, "IX_UserHr_Name").IsUnique();

            entity.Property(e => e.Id).HasColumnType("STRING");
            entity.Property(e => e.Company).HasColumnType("STRING");
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
            entity.Property(e => e.Email).HasColumnType("STRING");
            entity.Property(e => e.Login).HasColumnType("STRING");
            entity.Property(e => e.Name).HasColumnType("STRING");
            entity.Property(e => e.Password).HasColumnType("VARCHAR");
            entity.Property(e => e.Phone).HasColumnType("VARCHAR");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
