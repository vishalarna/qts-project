using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class AddProviderIdToMetaILA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProviderId",
                table: "MetaILAs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MetaILAs_ProviderId",
                table: "MetaILAs",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_MetaILAs_Providers_ProviderId",
                table: "MetaILAs",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MetaILAs_Providers_ProviderId",
                table: "MetaILAs");

            migrationBuilder.DropIndex(
                name: "IX_MetaILAs_ProviderId",
                table: "MetaILAs");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "MetaILAs");
        }
    }
}
