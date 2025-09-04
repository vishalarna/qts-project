using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class AddSuggestionAndQuestionPropertiesToILAAndClassTQSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowTaskQuestions",
                table: "TQILAEmpSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowTaskSuggestions",
                table: "TQILAEmpSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowTaskQuestions",
                table: "ClassSchedule_TQEMPSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowTaskSuggestions",
                table: "ClassSchedule_TQEMPSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowTaskQuestions",
                table: "TQILAEmpSettings");

            migrationBuilder.DropColumn(
                name: "ShowTaskSuggestions",
                table: "TQILAEmpSettings");

            migrationBuilder.DropColumn(
                name: "ShowTaskQuestions",
                table: "ClassSchedule_TQEMPSettings");

            migrationBuilder.DropColumn(
                name: "ShowTaskSuggestions",
                table: "ClassSchedule_TQEMPSettings");
        }
    }
}
