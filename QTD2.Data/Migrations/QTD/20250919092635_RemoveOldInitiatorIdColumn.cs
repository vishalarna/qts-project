using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class RemoveOldInitiatorIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SimulatorScenario_Scripts_Positions_InitiatorId",
                table: "SimulatorScenario_Scripts");

            migrationBuilder.DropIndex(
                name: "IX_SimulatorScenario_Scripts_InitiatorId",
                table: "SimulatorScenario_Scripts");

            migrationBuilder.DropColumn(
                name: "InitiatorId",
                table: "SimulatorScenario_Scripts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InitiatorId",
                table: "SimulatorScenario_Scripts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenario_Scripts_InitiatorId",
                table: "SimulatorScenario_Scripts",
                column: "InitiatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_SimulatorScenario_Scripts_Positions_InitiatorId",
                table: "SimulatorScenario_Scripts",
                column: "InitiatorId",
                principalTable: "Positions",
                principalColumn: "Id");
        }
    }
}
