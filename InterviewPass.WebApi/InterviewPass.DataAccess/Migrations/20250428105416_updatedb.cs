using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewPass.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Field",
                columns: table => new
                {
                    Id = table.Column<string>(type: "STRING", nullable: false),
                    Name = table.Column<string>(type: "STRING", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserHr",
                columns: table => new
                {
                    Id = table.Column<string>(type: "STRING", nullable: false),
                    Company = table.Column<string>(type: "STRING", nullable: true),
                    Login = table.Column<string>(type: "STRING", nullable: true),
                    Name = table.Column<string>(type: "STRING", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Email = table.Column<string>(type: "STRING", nullable: true),
                    Phone = table.Column<string>(type: "STRING", nullable: true),
                    PasswordHash = table.Column<string>(type: "STRING", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHr", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserJobSeeker",
                columns: table => new
                {
                    Id = table.Column<string>(type: "STRING", nullable: false),
                    LevelOfExperience = table.Column<int>(type: "INTEGER", nullable: true),
                    Login = table.Column<string>(type: "STRING", nullable: true),
                    Name = table.Column<string>(type: "STRING", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Email = table.Column<string>(type: "STRING", nullable: true),
                    Phone = table.Column<string>(type: "VARCHAR", nullable: true),
                    PasswordHash = table.Column<string>(type: "VARCHAR", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserJobSeeker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR", nullable: false),
                    Name = table.Column<string>(type: "STRING", nullable: false),
                    FieldId = table.Column<string>(type: "STRING", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skill_Field_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Field",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    Id = table.Column<string>(type: "STRING", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR", nullable: false),
                    Description = table.Column<string>(type: "STRING", nullable: true),
                    MinScore = table.Column<double>(type: "DOUBLE", nullable: true),
                    MaxScore = table.Column<double>(type: "DOUBLE", nullable: true),
                    DeadLineInNbrOfDays = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "STRING", nullable: true),
                    NbrOfQuestion = table.Column<int>(type: "INTEGER", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exam_UserHr_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "UserHr",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<string>(type: "STRING", nullable: false),
                    Content = table.Column<string>(type: "STRING", nullable: true),
                    Score = table.Column<double>(type: "DOUBLE", nullable: true),
                    Type = table.Column<string>(type: "STRING", maxLength: 21, nullable: false),
                    SkillId = table.Column<string>(type: "VARCHAR", nullable: true),
                    HasSignleChoice = table.Column<bool>(type: "BOOLEAN", nullable: true),
                    IsTrue = table.Column<bool>(type: "BOOLEAN", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SkillBySeeker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SkillId = table.Column<string>(type: "STRING", nullable: true),
                    JobSeekerId = table.Column<string>(type: "STRING", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillBySeeker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillBySeeker_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillBySeeker_UserJobSeeker_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "UserJobSeeker",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<string>(type: "STRING", nullable: false),
                    Status = table.Column<string>(type: "STRING", nullable: true),
                    ExamId = table.Column<string>(type: "STRING", nullable: true),
                    Score = table.Column<double>(type: "DOUBLE", nullable: true),
                    UserId = table.Column<string>(type: "STRING", nullable: true),
                    DeadLine = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CandidateSucceeded = table.Column<bool>(type: "BOOLEAN", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Exam_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Results_UserJobSeeker_UserId",
                        column: x => x.UserId,
                        principalTable: "UserJobSeeker",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Possibilities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "STRING", nullable: false),
                    QuestionId = table.Column<string>(type: "STRING", nullable: true),
                    IsTheCorrectAnswer = table.Column<bool>(type: "BOOLEAN", nullable: true),
                    Content = table.Column<string>(type: "STRING", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Possibilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Possibilities_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuestionExams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdExam = table.Column<string>(type: "STRING", nullable: false),
                    IdQuestion = table.Column<string>(type: "STRING", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionExams_Exam_IdExam",
                        column: x => x.IdExam,
                        principalTable: "Exam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuestionExams_Question_IdQuestion",
                        column: x => x.IdQuestion,
                        principalTable: "Question",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "STRING", nullable: false),
                    ExamId = table.Column<string>(type: "STRING", nullable: true),
                    QuestionId = table.Column<string>(type: "STRING", nullable: true),
                    Type = table.Column<string>(type: "STRING", nullable: true),
                    ObtainedScore = table.Column<double>(type: "DOUBLE", nullable: true),
                    UserId = table.Column<string>(type: "STRING", nullable: true),
                    IsTrueSelected = table.Column<bool>(type: "BOOLEAN", nullable: true),
                    TextAnswer = table.Column<string>(type: "STRING", nullable: true),
                    ImageAnswer = table.Column<byte[]>(type: "BLOB", nullable: true),
                    ZipAnswer = table.Column<byte[]>(type: "BLOB", nullable: true),
                    GitHubLink = table.Column<string>(type: "STRING", nullable: true),
                    ResultId = table.Column<string>(type: "STRING", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answer_Exam_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Answer_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Answer_Results_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Results",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Answer_UserJobSeeker_UserId",
                        column: x => x.UserId,
                        principalTable: "UserJobSeeker",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SelectedPossibilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdPossibility = table.Column<string>(type: "STRING", nullable: true),
                    IdAnswer = table.Column<string>(type: "STRING", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedPossibilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectedPossibilities_Answer_IdAnswer",
                        column: x => x.IdAnswer,
                        principalTable: "Answer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SelectedPossibilities_Possibilities_IdPossibility",
                        column: x => x.IdPossibility,
                        principalTable: "Possibilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_ExamId",
                table: "Answer",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_ResultId",
                table: "Answer",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_UserId",
                table: "Answer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_CreatedBy",
                table: "Exam",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_Name",
                table: "Exam",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Field_Name",
                table: "Field",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Possibilities_QuestionId",
                table: "Possibilities",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_SkillId",
                table: "Question",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionExams_IdExam",
                table: "QuestionExams",
                column: "IdExam");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionExams_IdQuestion",
                table: "QuestionExams",
                column: "IdQuestion");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ExamId",
                table: "Results",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_UserId",
                table: "Results",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedPossibilities_IdAnswer",
                table: "SelectedPossibilities",
                column: "IdAnswer");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedPossibilities_IdPossibility",
                table: "SelectedPossibilities",
                column: "IdPossibility");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_FieldId",
                table: "Skill",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Name",
                table: "Skill",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkillBySeeker_JobSeekerId",
                table: "SkillBySeeker",
                column: "JobSeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillBySeeker_SkillId",
                table: "SkillBySeeker",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHr_Login",
                table: "UserHr",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserJobSeeker_Name",
                table: "UserJobSeeker",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionExams");

            migrationBuilder.DropTable(
                name: "SelectedPossibilities");

            migrationBuilder.DropTable(
                name: "SkillBySeeker");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Possibilities");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropTable(
                name: "UserJobSeeker");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "UserHr");

            migrationBuilder.DropTable(
                name: "Field");
        }
    }
}
