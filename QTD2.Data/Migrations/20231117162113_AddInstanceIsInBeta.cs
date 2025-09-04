using Microsoft.EntityFrameworkCore.Migrations;

namespace QTD2.Data.Migrations
{
    public partial class AddInstanceIsInBeta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInBeta",
                table: "Instances",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInBeta",
                table: "Instances");
        }
    }
}
