using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class UpdateDataFromProcILAToILAProcTableAndAddNewFilterForReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Initialization.QTDContext.SeedData seed = new Initialization.QTDContext.SeedData(
            System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
            migrationBuilder);
            seed.MigrateDataFromProcedure_ILA_LinksToILA_Procedure_Links();
            seed.AddNewReportFilters_AccordingToRRTTypes();
            seed.UpdateReportFilters_ILAByProvider();
            seed.UpdateReport_ILAByCompletionHistory();
            seed.UpdateReport_TaskByPosition();
            seed.UpdateReport_TrainingIssueList();
            seed.AddReport_TrainingIssueDetails();
            seed.AddReport_TrainingIssuesActionItems();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
