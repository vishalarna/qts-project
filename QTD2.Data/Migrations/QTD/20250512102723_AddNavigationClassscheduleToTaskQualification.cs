using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class AddNavigationClassscheduleToTaskQualification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TaskQualifications_ClassScheduleId",
                table: "TaskQualifications",
                column: "ClassScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskQualifications_ClassSchedules_ClassScheduleId",
                table: "TaskQualifications",
                column: "ClassScheduleId",
                principalTable: "ClassSchedules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskQualifications_ClassSchedules_ClassScheduleId",
                table: "TaskQualifications");

            migrationBuilder.DropIndex(
                name: "IX_TaskQualifications_ClassScheduleId",
                table: "TaskQualifications");
        }
    }
}
