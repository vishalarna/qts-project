using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class TrainingIssueDataElementMappingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingIssueDataElements_TrainingIssues_TrainingIssueId",
                table: "TrainingIssueDataElements");

            migrationBuilder.DropIndex(
                name: "IX_TrainingIssueDataElements_TrainingIssueId",
                table: "TrainingIssueDataElements");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingIssueDataElements_TrainingIssueId",
                table: "TrainingIssueDataElements",
                column: "TrainingIssueId",
                unique: true,
                filter: "[Deleted] = 0");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingIssueDataElements_TrainingIssues_TrainingIssueId",
                table: "TrainingIssueDataElements",
                column: "TrainingIssueId",
                principalTable: "TrainingIssues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingIssueDataElements_TrainingIssues_TrainingIssueId",
                table: "TrainingIssueDataElements");

            migrationBuilder.DropIndex(
                name: "IX_TrainingIssueDataElements_TrainingIssueId",
                table: "TrainingIssueDataElements");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingIssueDataElements_TrainingIssueId",
                table: "TrainingIssueDataElements",
                column: "TrainingIssueId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingIssueDataElements_TrainingIssues_TrainingIssueId",
                table: "TrainingIssueDataElements",
                column: "TrainingIssueId",
                principalTable: "TrainingIssues",
                principalColumn: "Id");
        }
    }
}
