using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class AddTQReleasePropertiesToClassScheduleTQEmpSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PriorToSpecificTime",
                table: "ClassSchedule_TQEMPSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ReleaseOnClassEnd",
                table: "ClassSchedule_TQEMPSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ReleaseOnClassStart",
                table: "ClassSchedule_TQEMPSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SpecificTime",
                table: "ClassSchedule_TQEMPSettings",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriorToSpecificTime",
                table: "ClassSchedule_TQEMPSettings");

            migrationBuilder.DropColumn(
                name: "ReleaseOnClassEnd",
                table: "ClassSchedule_TQEMPSettings");

            migrationBuilder.DropColumn(
                name: "ReleaseOnClassStart",
                table: "ClassSchedule_TQEMPSettings");

            migrationBuilder.DropColumn(
                name: "SpecificTime",
                table: "ClassSchedule_TQEMPSettings");
        }
    }
}
