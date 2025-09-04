using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class AddSkillQualificationAndRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SkillQualificationStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillQualificationStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkillQualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnablingObjectiveId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EvaluationMethodId = table.Column<int>(type: "int", nullable: true),
                    SkillQualificationStatusId = table.Column<int>(type: "int", nullable: true),
                    ClassScheduleId = table.Column<int>(type: "int", nullable: true),
                    SkillQualificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecallDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CriteriaMet = table.Column<bool>(type: "bit", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReleasedToEMP = table.Column<bool>(type: "bit", nullable: false),
                    IsRecalled = table.Column<bool>(type: "bit", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillQualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillQualifications_ClassSchedules_ClassScheduleId",
                        column: x => x.ClassScheduleId,
                        principalTable: "ClassSchedules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillQualifications_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillQualifications_EnablingObjectives_EnablingObjectiveId",
                        column: x => x.EnablingObjectiveId,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillQualifications_EvaluationMethods_EvaluationMethodId",
                        column: x => x.EvaluationMethodId,
                        principalTable: "EvaluationMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillQualifications_SkillQualificationStatus_SkillQualificationStatusId",
                        column: x => x.SkillQualificationStatusId,
                        principalTable: "SkillQualificationStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SkillQualification_Evaluator_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvaluatorId = table.Column<int>(type: "int", nullable: false),
                    SkillQualificationId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillQualification_Evaluator_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillQualification_Evaluator_Links_Employees_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillQualification_Evaluator_Links_SkillQualifications_SkillQualificationId",
                        column: x => x.SkillQualificationId,
                        principalTable: "SkillQualifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkillQualification_Evaluator_Links_EvaluatorId",
                table: "SkillQualification_Evaluator_Links",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillQualification_Evaluator_Links_SkillQualificationId",
                table: "SkillQualification_Evaluator_Links",
                column: "SkillQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillQualifications_ClassScheduleId",
                table: "SkillQualifications",
                column: "ClassScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillQualifications_EmployeeId",
                table: "SkillQualifications",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillQualifications_EnablingObjectiveId",
                table: "SkillQualifications",
                column: "EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillQualifications_EvaluationMethodId",
                table: "SkillQualifications",
                column: "EvaluationMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillQualifications_SkillQualificationStatusId",
                table: "SkillQualifications",
                column: "SkillQualificationStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkillQualification_Evaluator_Links");

            migrationBuilder.DropTable(
                name: "SkillQualifications");

            migrationBuilder.DropTable(
                name: "SkillQualificationStatus");
        }
    }
}
