using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class RenameSimulatorScenarioPositionIdToInitiatorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SimulatorScenario_Scripts_SimulatorScenario_Positions_SimulatorScenario_PositionId",
                table: "SimulatorScenario_Scripts");

            migrationBuilder.RenameColumn(
                name: "SimulatorScenario_PositionId",
                table: "SimulatorScenario_Scripts",
                newName: "InitiatorId");

            migrationBuilder.RenameIndex(
                name: "IX_SimulatorScenario_Scripts_SimulatorScenario_PositionId",
                table: "SimulatorScenario_Scripts",
                newName: "IX_SimulatorScenario_Scripts_InitiatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_SimulatorScenario_Scripts_SimulatorScenario_Positions_InitiatorId",
                table: "SimulatorScenario_Scripts",
                column: "InitiatorId",
                principalTable: "SimulatorScenario_Positions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SimulatorScenario_Scripts_SimulatorScenario_Positions_InitiatorId",
                table: "SimulatorScenario_Scripts");

            migrationBuilder.RenameColumn(
                name: "InitiatorId",
                table: "SimulatorScenario_Scripts",
                newName: "SimulatorScenario_PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_SimulatorScenario_Scripts_InitiatorId",
                table: "SimulatorScenario_Scripts",
                newName: "IX_SimulatorScenario_Scripts_SimulatorScenario_PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SimulatorScenario_Scripts_SimulatorScenario_Positions_SimulatorScenario_PositionId",
                table: "SimulatorScenario_Scripts",
                column: "SimulatorScenario_PositionId",
                principalTable: "SimulatorScenario_Positions",
                principalColumn: "Id");
        }
    }
}
