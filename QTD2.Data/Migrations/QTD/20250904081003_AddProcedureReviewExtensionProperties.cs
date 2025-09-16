using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class AddProcedureReviewExtensionProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExtensionAmount",
                table: "ProcedureReviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExtensionType",
                table: "ProcedureReviews",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtensionAmount",
                table: "ProcedureReviews");

            migrationBuilder.DropColumn(
                name: "ExtensionType",
                table: "ProcedureReviews");
        }
    }
}
