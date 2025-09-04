using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class AddFieldsToIDPSchedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "IDPSchedules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "IDPSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GradeReason",
                table: "IDPSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Score",
                table: "IDPSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TaskQualificationCompleted",
                table: "IDPSchedules",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "IDPSchedules");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "IDPSchedules");

            migrationBuilder.DropColumn(
                name: "GradeReason",
                table: "IDPSchedules");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "IDPSchedules");

            migrationBuilder.DropColumn(
                name: "TaskQualificationCompleted",
                table: "IDPSchedules");
        }
    }
}
