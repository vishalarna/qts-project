using Microsoft.EntityFrameworkCore.Migrations;

namespace QTD2.Data.Migrations
{
    public partial class AddInstanceSettingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstanceSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstanceId = table.Column<int>(type: "int", nullable: false),
                    DatabaseName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataBaseVersion = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "1"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstanceSettings_Instances_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "Instances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstanceSettings_InstanceId",
                table: "InstanceSettings",
                column: "InstanceId",
                unique: true);

            Initialization.QTDAuthentication.SeedData seed =
           new Initialization.QTDAuthentication.SeedData(
           System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
           migrationBuilder);

            seed.AddInstanceSettingsTable();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstanceSettings");
        }
    }
}
