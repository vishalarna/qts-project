using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class UpdateReport_EmployeeTowardNercCertificationReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Initialization.QTDContext.SeedData seed = new Initialization.QTDContext.SeedData(
            System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
            migrationBuilder);
            seed.AddNewReportFilters_EmployeeTrainingTowardNERCRecertification();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
