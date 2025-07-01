using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewPass.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema_V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PossibilityId",
                table: "SelectedPossibilities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Benefits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "STRING", nullable: false),
                    Name = table.Column<string>(type: "STRING", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benefits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "STRING", nullable: false),
                    Type = table.Column<string>(type: "STRING", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    Id = table.Column<string>(type: "STRING", nullable: false),
                    Title = table.Column<string>(type: "STRING", nullable: true),
                    ShortDescription = table.Column<string>(type: "STRING", nullable: true),
                    ImagePath = table.Column<string>(type: "STRING", nullable: true),
                    Experience = table.Column<int>(type: "INTEGER", nullable: true),
                    WorkingSchedule = table.Column<string>(type: "STRING", nullable: true),
                    Role = table.Column<string>(type: "STRING", nullable: true),
                    Salary = table.Column<double>(type: "DOUBLE", nullable: true),
                    EmploymentTypeId = table.Column<string>(type: "STRING", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Job_EmploymentType_EmploymentTypeId",
                        column: x => x.EmploymentTypeId,
                        principalTable: "EmploymentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobBenefit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "STRING", nullable: false),
                    JobId = table.Column<string>(type: "STRING", nullable: false),
                    BenefitsId = table.Column<string>(type: "STRING", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobBenefit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobBenefit_Benefits_BenefitsId",
                        column: x => x.BenefitsId,
                        principalTable: "Benefits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobBenefit_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobFile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "STRING", nullable: false),
                    FilePath = table.Column<string>(type: "STRING", nullable: true),
                    FileName = table.Column<string>(type: "STRING", nullable: true),
                    JobId = table.Column<string>(type: "STRING", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobFile_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobSkill",
                columns: table => new
                {
                    Id = table.Column<string>(type: "STRING", nullable: false),
                    JobId = table.Column<string>(type: "STRING", nullable: true),
                    SkillId = table.Column<string>(type: "STRING", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSkill_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobSkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Job_EmploymentTypeId",
                table: "Job",
                column: "EmploymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Title",
                table: "Job",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_JobBenefit_BenefitsId",
                table: "JobBenefit",
                column: "BenefitsId");

            migrationBuilder.CreateIndex(
                name: "IX_JobBenefit_JobId",
                table: "JobBenefit",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobFile_JobId",
                table: "JobFile",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkill_JobId",
                table: "JobSkill",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkill_SkillId",
                table: "JobSkill",
                column: "SkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobBenefit");

            migrationBuilder.DropTable(
                name: "JobFile");

            migrationBuilder.DropTable(
                name: "JobSkill");

            migrationBuilder.DropTable(
                name: "Benefits");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "EmploymentType");

            migrationBuilder.DropColumn(
                name: "PossibilityId",
                table: "SelectedPossibilities");
        }
    }
}
