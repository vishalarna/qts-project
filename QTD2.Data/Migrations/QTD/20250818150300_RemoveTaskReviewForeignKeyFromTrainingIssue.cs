using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class RemoveTaskReviewForeignKeyFromTrainingIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingIssues_TaskReview_TaskReviewId",
                table: "TrainingIssues");

            migrationBuilder.DropIndex(
                name: "IX_TrainingIssues_TaskReviewId",
                table: "TrainingIssues");

            migrationBuilder.CreateIndex(
                name: "IX_TaskReview_TrainingIssueId",
                table: "TaskReview",
                column: "TrainingIssueId",
                unique: true,
                filter: "[TrainingIssueId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskReview_TrainingIssues_TrainingIssueId",
                table: "TaskReview",
                column: "TrainingIssueId",
                principalTable: "TrainingIssues",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskReview_TrainingIssues_TrainingIssueId",
                table: "TaskReview");

            migrationBuilder.DropIndex(
                name: "IX_TaskReview_TrainingIssueId",
                table: "TaskReview");

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
    }
}
