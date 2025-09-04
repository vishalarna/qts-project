using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableData_IdentityProviderAndLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Initialization.QTDAuthentication.SeedData seed = new Initialization.QTDAuthentication.SeedData(
                     System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                     migrationBuilder);
            seed.AddTableData_IdentityProviderAndLinks();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
