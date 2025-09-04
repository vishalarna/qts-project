using Microsoft.EntityFrameworkCore.Migrations;

namespace QTD2.Data.Migrations
{
    public partial class AddEventLog_SystemId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SystemId",
                table: "EventLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "EventLogs");
        }
    }
}
