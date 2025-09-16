using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class UpdateateSkillReQualificationEmp_QuestionAnswerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillReQualificationEmp_QuestionAnswers_EnablingObjective_Questions_SkillQualificationId",
                table: "SkillReQualificationEmp_QuestionAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_SkillReQualificationEmp_QuestionAnswers_SkillQuestionId",
                table: "SkillReQualificationEmp_QuestionAnswers",
                column: "SkillQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillReQualificationEmp_QuestionAnswers_EnablingObjective_Questions_SkillQuestionId",
                table: "SkillReQualificationEmp_QuestionAnswers",
                column: "SkillQuestionId",
                principalTable: "EnablingObjective_Questions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillReQualificationEmp_QuestionAnswers_EnablingObjective_Questions_SkillQuestionId",
                table: "SkillReQualificationEmp_QuestionAnswers");

            migrationBuilder.DropIndex(
                name: "IX_SkillReQualificationEmp_QuestionAnswers_SkillQuestionId",
                table: "SkillReQualificationEmp_QuestionAnswers");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillReQualificationEmp_QuestionAnswers_EnablingObjective_Questions_SkillQualificationId",
                table: "SkillReQualificationEmp_QuestionAnswers",
                column: "SkillQualificationId",
                principalTable: "EnablingObjective_Questions",
                principalColumn: "Id");
        }
    }
}
