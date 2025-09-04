using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class RemoveAssigneeToPropertyFromTrainingIssueActionItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingIssueActionItems_QTDUsers_AssignedToId",
                table: "TrainingIssueActionItems");

            migrationBuilder.DropIndex(
                name: "IX_TrainingIssueActionItems_AssignedToId",
                table: "TrainingIssueActionItems");

            migrationBuilder.DropColumn(
                name: "AssignedToId",
                table: "TrainingIssueActionItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedToId",
                table: "TrainingIssueActionItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingIssueActionItems_AssignedToId",
                table: "TrainingIssueActionItems",
                column: "AssignedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingIssueActionItems_QTDUsers_AssignedToId",
                table: "TrainingIssueActionItems",
                column: "AssignedToId",
                principalTable: "QTDUsers",
                principalColumn: "Id");
        }
    }
}
