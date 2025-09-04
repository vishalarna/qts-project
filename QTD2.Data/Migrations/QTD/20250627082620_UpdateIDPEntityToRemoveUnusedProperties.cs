using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class UpdateIDPEntityToRemoveUnusedProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "IDPs");

            migrationBuilder.DropColumn(
                name: "GradeUpdateReason",
                table: "IDPs");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "IDPs");

            migrationBuilder.DropColumn(
                name: "completionDate",
                table: "IDPs");

            migrationBuilder.DropColumn(
                name: "taskQualificationCompleted",
                table: "IDPs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "IDPs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GradeUpdateReason",
                table: "IDPs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Score",
                table: "IDPs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "completionDate",
                table: "IDPs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "taskQualificationCompleted",
                table: "IDPs",
                type: "bit",
                nullable: true);
        }
    }
}
