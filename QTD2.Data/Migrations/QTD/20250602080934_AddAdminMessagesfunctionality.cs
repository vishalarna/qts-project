using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class AddAdminMessagesfunctionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceAdminMessageId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminMessage_QTDUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminMessageId = table.Column<int>(type: "int", nullable: false),
                    QTDUserId = table.Column<int>(type: "int", nullable: false),
                    Dismissed = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminMessage_QTDUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminMessage_QTDUsers_AdminMessages_AdminMessageId",
                        column: x => x.AdminMessageId,
                        principalTable: "AdminMessages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdminMessage_QTDUsers_QTDUsers_QTDUserId",
                        column: x => x.QTDUserId,
                        principalTable: "QTDUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminMessage_QTDUsers_AdminMessageId",
                table: "AdminMessage_QTDUsers",
                column: "AdminMessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdminMessage_QTDUsers_QTDUserId",
                table: "AdminMessage_QTDUsers",
                column: "QTDUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminMessage_QTDUsers");

            migrationBuilder.DropTable(
                name: "AdminMessages");
        }
    }
}
