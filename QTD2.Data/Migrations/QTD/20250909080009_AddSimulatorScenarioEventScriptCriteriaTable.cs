using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class AddSimulatorScenarioEventScriptCriteriaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SimulatorScenario_Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SimulatorScenarioId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulatorScenario_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulatorScenario_Events_SimulatorScenarios_SimulatorScenarioId",
                        column: x => x.SimulatorScenarioId,
                        principalTable: "SimulatorScenarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SimulatorScenario_Scripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitiatorId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulatorScenario_Scripts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulatorScenario_Scripts_Positions_InitiatorId",
                        column: x => x.InitiatorId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SimulatorScenario_Scripts_SimulatorScenario_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "SimulatorScenario_Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SimulatorScenario_Script_Criterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScriptId = table.Column<int>(type: "int", nullable: true),
                    CriteriaId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulatorScenario_Script_Criterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulatorScenario_Script_Criterias_SimulatorScenario_Scripts_ScriptId",
                        column: x => x.ScriptId,
                        principalTable: "SimulatorScenario_Scripts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SimulatorScenario_Script_Criterias_SimulatorScenario_Task_Criterias_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "SimulatorScenario_Task_Criterias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenario_Events_SimulatorScenarioId",
                table: "SimulatorScenario_Events",
                column: "SimulatorScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenario_Script_Criterias_CriteriaId",
                table: "SimulatorScenario_Script_Criterias",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenario_Script_Criterias_ScriptId",
                table: "SimulatorScenario_Script_Criterias",
                column: "ScriptId");

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenario_Scripts_EventId",
                table: "SimulatorScenario_Scripts",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenario_Scripts_InitiatorId",
                table: "SimulatorScenario_Scripts",
                column: "InitiatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimulatorScenario_Script_Criterias");

            migrationBuilder.DropTable(
                name: "SimulatorScenario_Scripts");

            migrationBuilder.DropTable(
                name: "SimulatorScenario_Events");
        }
    }
}
