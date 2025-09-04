using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class UpdateAdminMessageQTDUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AdminMessage_QTDUsers_AdminMessageId",
                table: "AdminMessage_QTDUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AdminMessage_QTDUsers_AdminMessageId",
                table: "AdminMessage_QTDUsers",
                column: "AdminMessageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AdminMessage_QTDUsers_AdminMessageId",
                table: "AdminMessage_QTDUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AdminMessage_QTDUsers_AdminMessageId",
                table: "AdminMessage_QTDUsers",
                column: "AdminMessageId",
                unique: true);
        }
    }
}
