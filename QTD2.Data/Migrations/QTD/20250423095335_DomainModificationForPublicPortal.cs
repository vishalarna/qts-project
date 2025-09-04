using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class DomainModificationForPublicPortal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PublicOrganization",
                table: "Organizations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPubliclyAvailable",
                table: "ILAs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ClientSettings_LicenseId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PublicUser",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPubliclyAvailable",
                table: "ClassSchedules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ClientSettings_LicenseId",
                table: "Employees",
                column: "ClientSettings_LicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_ClientSettings_Licenses_ClientSettings_LicenseId",
                table: "Employees",
                column: "ClientSettings_LicenseId",
                principalTable: "ClientSettings_Licenses",
                principalColumn: "Id");

            Initialization.QTDContext.SeedData seed = new Initialization.QTDContext.SeedData(
            System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
            migrationBuilder);
            seed.AddClientSettings_Feature_PublicClasses();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_ClientSettings_Licenses_ClientSettings_LicenseId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ClientSettings_LicenseId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PublicOrganization",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "IsPubliclyAvailable",
                table: "ILAs");

            migrationBuilder.DropColumn(
                name: "ClientSettings_LicenseId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PublicUser",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsPubliclyAvailable",
                table: "ClassSchedules");
        }
    }
}
