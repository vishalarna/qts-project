using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityProviderPropToInstanceSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultIdentityProviderId",
                table: "InstanceSettings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstanceSettings_DefaultIdentityProviderId",
                table: "InstanceSettings",
                column: "DefaultIdentityProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstanceSettings_IdentityProviders_DefaultIdentityProviderId",
                table: "InstanceSettings",
                column: "DefaultIdentityProviderId",
                principalTable: "IdentityProviders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstanceSettings_IdentityProviders_DefaultIdentityProviderId",
                table: "InstanceSettings");

            migrationBuilder.DropIndex(
                name: "IX_InstanceSettings_DefaultIdentityProviderId",
                table: "InstanceSettings");

            migrationBuilder.DropColumn(
                name: "DefaultIdentityProviderId",
                table: "InstanceSettings");
        }
    }
}
