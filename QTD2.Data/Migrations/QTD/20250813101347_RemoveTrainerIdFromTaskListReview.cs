using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class RemoveTrainerIdFromTaskListReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskListReview_QTDUsers_TrainerId",
                table: "TaskListReview");

            migrationBuilder.DropIndex(
                name: "IX_TaskListReview_TrainerId",
                table: "TaskListReview");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "TaskListReview");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "TaskListReview",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskListReview_TrainerId",
                table: "TaskListReview",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskListReview_QTDUsers_TrainerId",
                table: "TaskListReview",
                column: "TrainerId",
                principalTable: "QTDUsers",
                principalColumn: "Id");
        }
    }
}
