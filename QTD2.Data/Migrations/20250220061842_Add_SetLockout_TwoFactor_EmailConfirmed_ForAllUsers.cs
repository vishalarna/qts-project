using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_SetLockout_TwoFactor_EmailConfirmed_ForAllUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Initialization.QTDAuthentication.SeedData seed = new Initialization.QTDAuthentication.SeedData(
                     System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                     migrationBuilder);
            seed.SetLockoutForAllUsers();
            seed.SetTwoFactorEnabledForAllUsers();
            seed.SetEmailConfirmedForAllUsers();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
