using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class RemoveSafetyHazardILALinksEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SafetyHazard_ILA_Links");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SafetyHazard_ILA_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ILAId = table.Column<int>(type: "int", nullable: false),
                    SafetyHazardId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyHazard_ILA_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SafetyHazard_ILA_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SafetyHazard_ILA_Links_SaftyHazards_SafetyHazardId",
                        column: x => x.SafetyHazardId,
                        principalTable: "SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SafetyHazard_ILA_Links_ILAId_SafetyHazardId",
                table: "SafetyHazard_ILA_Links",
                columns: new[] { "ILAId", "SafetyHazardId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SafetyHazard_ILA_Links_SafetyHazardId",
                table: "SafetyHazard_ILA_Links",
                column: "SafetyHazardId");
        }
    }
}
