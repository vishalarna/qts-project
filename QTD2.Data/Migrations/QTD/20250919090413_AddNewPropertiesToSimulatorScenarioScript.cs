using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class AddNewPropertiesToSimulatorScenarioScript : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InitiatorInstructor",
                table: "SimulatorScenario_Scripts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InitiatorOther",
                table: "SimulatorScenario_Scripts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SimulatorScenario_PositionId",
                table: "SimulatorScenario_Scripts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenario_Scripts_SimulatorScenario_PositionId",
                table: "SimulatorScenario_Scripts",
                column: "SimulatorScenario_PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SimulatorScenario_Scripts_SimulatorScenario_Positions_SimulatorScenario_PositionId",
                table: "SimulatorScenario_Scripts",
                column: "SimulatorScenario_PositionId",
                principalTable: "SimulatorScenario_Positions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SimulatorScenario_Scripts_SimulatorScenario_Positions_SimulatorScenario_PositionId",
                table: "SimulatorScenario_Scripts");

            migrationBuilder.DropIndex(
                name: "IX_SimulatorScenario_Scripts_SimulatorScenario_PositionId",
                table: "SimulatorScenario_Scripts");

            migrationBuilder.DropColumn(
                name: "InitiatorInstructor",
                table: "SimulatorScenario_Scripts");

            migrationBuilder.DropColumn(
                name: "InitiatorOther",
                table: "SimulatorScenario_Scripts");

            migrationBuilder.DropColumn(
                name: "SimulatorScenario_PositionId",
                table: "SimulatorScenario_Scripts");
        }
    }
}
