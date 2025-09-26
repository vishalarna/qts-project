using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class UpdateNotificationWithEMPSkillQualificationTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkillQualificationId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SkillQualification_Evaluator_LinkId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SkillQualification_Evaluator_LinkId",
                table: "Notifications",
                column: "SkillQualification_Evaluator_LinkId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SkillQualificationId",
                table: "Notifications",
                column: "SkillQualificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_SkillQualification_Evaluator_Links_SkillQualification_Evaluator_LinkId",
                table: "Notifications",
                column: "SkillQualification_Evaluator_LinkId",
                principalTable: "SkillQualification_Evaluator_Links",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_SkillQualifications_SkillQualificationId",
                table: "Notifications",
                column: "SkillQualificationId",
                principalTable: "SkillQualifications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_SkillQualification_Evaluator_Links_SkillQualification_Evaluator_LinkId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_SkillQualifications_SkillQualificationId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_SkillQualification_Evaluator_LinkId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_SkillQualificationId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "SkillQualificationId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "SkillQualification_Evaluator_LinkId",
                table: "Notifications");
        }
    }
}
