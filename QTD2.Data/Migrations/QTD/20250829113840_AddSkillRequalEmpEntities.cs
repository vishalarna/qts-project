using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class AddSkillRequalEmpEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SkillQualificationEmp_SignOffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillQualificationId = table.Column<int>(type: "int", nullable: false),
                    IsCriteriaMet = table.Column<bool>(type: "bit", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluatorId = table.Column<int>(type: "int", nullable: false),
                    EvaluationMethodId = table.Column<int>(type: "int", nullable: true),
                    SkillQualificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: true),
                    IsStarted = table.Column<bool>(type: "bit", nullable: true),
                    IsLocked = table.Column<bool>(type: "bit", nullable: true),
                    SignOffDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsTraineeSignOff = table.Column<bool>(type: "bit", nullable: true),
                    IsEvaluatorSignOff = table.Column<bool>(type: "bit", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillQualificationEmp_SignOffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillQualificationEmp_SignOffs_Employees_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillQualificationEmp_SignOffs_Employees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillQualificationEmp_SignOffs_EvaluationMethods_EvaluationMethodId",
                        column: x => x.EvaluationMethodId,
                        principalTable: "EvaluationMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillQualificationEmp_SignOffs_SkillQualifications_SkillQualificationId",
                        column: x => x.SkillQualificationId,
                        principalTable: "SkillQualifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SkillReQualificationEmp_QuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillQualificationId = table.Column<int>(type: "int", nullable: false),
                    SkillQuestionId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluatorId = table.Column<int>(type: "int", nullable: false),
                    CommentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillReQualificationEmp_QuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillReQualificationEmp_QuestionAnswers_Employees_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillReQualificationEmp_QuestionAnswers_Employees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillReQualificationEmp_QuestionAnswers_EnablingObjective_Questions_SkillQualificationId",
                        column: x => x.SkillQualificationId,
                        principalTable: "EnablingObjective_Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillReQualificationEmp_QuestionAnswers_SkillQualifications_SkillQualificationId",
                        column: x => x.SkillQualificationId,
                        principalTable: "SkillQualifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SkillReQualificationEmp_Steps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillQualificationId = table.Column<int>(type: "int", nullable: false),
                    SkillStepId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluatorId = table.Column<int>(type: "int", nullable: false),
                    CommentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillReQualificationEmp_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillReQualificationEmp_Steps_Employees_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillReQualificationEmp_Steps_Employees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillReQualificationEmp_Steps_EnablingObjective_Steps_SkillStepId",
                        column: x => x.SkillStepId,
                        principalTable: "EnablingObjective_Steps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillReQualificationEmp_Steps_SkillQualifications_SkillQualificationId",
                        column: x => x.SkillQualificationId,
                        principalTable: "SkillQualifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SkillReQualificationEmp_Suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillQualificationId = table.Column<int>(type: "int", nullable: false),
                    SkillSuggestionId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluatorId = table.Column<int>(type: "int", nullable: false),
                    CommentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillReQualificationEmp_Suggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillReQualificationEmp_Suggestions_Employees_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillReQualificationEmp_Suggestions_Employees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillReQualificationEmp_Suggestions_EnablingObjective_Suggestions_SkillSuggestionId",
                        column: x => x.SkillSuggestionId,
                        principalTable: "EnablingObjective_Suggestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillReQualificationEmp_Suggestions_SkillQualifications_SkillQualificationId",
                        column: x => x.SkillQualificationId,
                        principalTable: "SkillQualifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkillQualificationEmp_SignOffs_EvaluationMethodId",
                table: "SkillQualificationEmp_SignOffs",
                column: "EvaluationMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillQualificationEmp_SignOffs_EvaluatorId",
                table: "SkillQualificationEmp_SignOffs",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillQualificationEmp_SignOffs_SkillQualificationId",
                table: "SkillQualificationEmp_SignOffs",
                column: "SkillQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillQualificationEmp_SignOffs_TraineeId",
                table: "SkillQualificationEmp_SignOffs",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillReQualificationEmp_QuestionAnswers_EvaluatorId",
                table: "SkillReQualificationEmp_QuestionAnswers",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillReQualificationEmp_QuestionAnswers_SkillQualificationId",
                table: "SkillReQualificationEmp_QuestionAnswers",
                column: "SkillQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillReQualificationEmp_QuestionAnswers_TraineeId",
                table: "SkillReQualificationEmp_QuestionAnswers",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillReQualificationEmp_Steps_EvaluatorId",
                table: "SkillReQualificationEmp_Steps",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillReQualificationEmp_Steps_SkillQualificationId",
                table: "SkillReQualificationEmp_Steps",
                column: "SkillQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillReQualificationEmp_Steps_SkillStepId",
                table: "SkillReQualificationEmp_Steps",
                column: "SkillStepId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillReQualificationEmp_Steps_TraineeId",
                table: "SkillReQualificationEmp_Steps",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillReQualificationEmp_Suggestions_EvaluatorId",
                table: "SkillReQualificationEmp_Suggestions",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillReQualificationEmp_Suggestions_SkillQualificationId",
                table: "SkillReQualificationEmp_Suggestions",
                column: "SkillQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillReQualificationEmp_Suggestions_SkillSuggestionId",
                table: "SkillReQualificationEmp_Suggestions",
                column: "SkillSuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillReQualificationEmp_Suggestions_TraineeId",
                table: "SkillReQualificationEmp_Suggestions",
                column: "TraineeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkillQualificationEmp_SignOffs");

            migrationBuilder.DropTable(
                name: "SkillReQualificationEmp_QuestionAnswers");

            migrationBuilder.DropTable(
                name: "SkillReQualificationEmp_Steps");

            migrationBuilder.DropTable(
                name: "SkillReQualificationEmp_Suggestions");
        }
    }
}
