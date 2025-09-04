using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations
{
    /// <inheritdoc />
    public partial class SetPublicUrlToClassesForAllUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Initialization.QTDAuthentication.SeedData seed = new Initialization.QTDAuthentication.SeedData(
                     System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                     migrationBuilder);
            seed.SetPublicUrlToClassesForAllUsers();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
