using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class RemovedPositionIdFromTaskListReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskListReview_Positions_PositionId",
                table: "TaskListReview");

            migrationBuilder.DropIndex(
                name: "IX_TaskListReview_PositionId",
                table: "TaskListReview");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "TaskListReview");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "TaskListReview",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskListReview_PositionId",
                table: "TaskListReview",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskListReview_Positions_PositionId",
                table: "TaskListReview",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");
        }
    }
}
