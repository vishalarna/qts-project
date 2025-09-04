using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class AddTaskReviewTrainingIssueLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskReviewId",
                table: "TrainingIssues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainingIssueId",
                table: "TaskReview",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingIssues_TaskReviewId",
                table: "TrainingIssues",
                column: "TaskReviewId",
                unique: true,
                filter: "[TaskReviewId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingIssues_TaskReview_TaskReviewId",
                table: "TrainingIssues",
                column: "TaskReviewId",
                principalTable: "TaskReview",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingIssues_TaskReview_TaskReviewId",
                table: "TrainingIssues");

            migrationBuilder.DropIndex(
                name: "IX_TrainingIssues_TaskReviewId",
                table: "TrainingIssues");

            migrationBuilder.DropColumn(
                name: "TaskReviewId",
                table: "TrainingIssues");

            migrationBuilder.DropColumn(
                name: "TrainingIssueId",
                table: "TaskReview");
        }
    }
}
