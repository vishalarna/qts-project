using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class RemoveSimulatorScenarioEventAndScriptEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimulatorScenario_EventAndScript_Criterias");

            migrationBuilder.DropTable(
                name: "SimulatorScenario_EventAndScripts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SimulatorScenario_EventAndScripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InitiatorId = table.Column<int>(type: "int", nullable: false),
                    SimulatorScenarioId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulatorScenario_EventAndScripts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulatorScenario_EventAndScripts_Positions_InitiatorId",
                        column: x => x.InitiatorId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SimulatorScenario_EventAndScripts_SimulatorScenarios_SimulatorScenarioId",
                        column: x => x.SimulatorScenarioId,
                        principalTable: "SimulatorScenarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SimulatorScenario_EventAndScript_Criterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriteriaId = table.Column<int>(type: "int", nullable: false),
                    EventAndScriptId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulatorScenario_EventAndScript_Criterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulatorScenario_EventAndScript_Criterias_SimulatorScenario_EventAndScripts_EventAndScriptId",
                        column: x => x.EventAndScriptId,
                        principalTable: "SimulatorScenario_EventAndScripts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SimulatorScenario_EventAndScript_Criterias_SimulatorScenario_Task_Criterias_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "SimulatorScenario_Task_Criterias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenario_EventAndScript_Criterias_CriteriaId",
                table: "SimulatorScenario_EventAndScript_Criterias",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenario_EventAndScript_Criterias_EventAndScriptId",
                table: "SimulatorScenario_EventAndScript_Criterias",
                column: "EventAndScriptId");

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenario_EventAndScripts_InitiatorId",
                table: "SimulatorScenario_EventAndScripts",
                column: "InitiatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenario_EventAndScripts_SimulatorScenarioId",
                table: "SimulatorScenario_EventAndScripts",
                column: "SimulatorScenarioId");
        }
    }
}
