using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class AddEntity_TrainingProgramReview_TrainingIssue_Link : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingProgramReview_TrainingIssue_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingProgramId = table.Column<int>(type: "int", nullable: false),
                    TrainingIssueId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingProgramReview_TrainingIssue_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingProgramReview_TrainingIssue_Links_TrainingIssues_TrainingIssueId",
                        column: x => x.TrainingIssueId,
                        principalTable: "TrainingIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingProgramReview_TrainingIssue_Links_TrainingPrograms_TrainingProgramId",
                        column: x => x.TrainingProgramId,
                        principalTable: "TrainingPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgramReview_TrainingIssue_Links_TrainingIssueId",
                table: "TrainingProgramReview_TrainingIssue_Links",
                column: "TrainingIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgramReview_TrainingIssue_Links_TrainingProgramId",
                table: "TrainingProgramReview_TrainingIssue_Links",
                column: "TrainingProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingProgramReview_TrainingIssue_Links");
        }
    }
}
