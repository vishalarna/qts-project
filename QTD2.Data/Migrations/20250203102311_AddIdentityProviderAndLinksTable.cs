using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityProviderAndLinksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SubType = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorizationEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserInformationEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailClaim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityIDUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaDataUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathToPfx = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instance_IdentityProvider_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstanceId = table.Column<int>(type: "int", nullable: false),
                    IdentityProviderId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instance_IdentityProvider_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instance_IdentityProvider_Links_IdentityProviders_IdentityProviderId",
                        column: x => x.IdentityProviderId,
                        principalTable: "IdentityProviders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Instance_IdentityProvider_Links_Instances_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "Instances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdentityProviders_Name",
                table: "IdentityProviders",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Instance_IdentityProvider_Links_IdentityProviderId",
                table: "Instance_IdentityProvider_Links",
                column: "IdentityProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Instance_IdentityProvider_Links_InstanceId",
                table: "Instance_IdentityProvider_Links",
                column: "InstanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instance_IdentityProvider_Links");

            migrationBuilder.DropTable(
                name: "IdentityProviders");
        }
    }
}
