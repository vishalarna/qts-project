using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class MigrateSimulatorScenarioEventsScriptsAndCriteria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Initialization.QTDContext.SeedData seed = new Initialization.QTDContext.SeedData(
                       System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                       migrationBuilder);
            seed.SeedSimulatorScenarioEventsAsync();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
