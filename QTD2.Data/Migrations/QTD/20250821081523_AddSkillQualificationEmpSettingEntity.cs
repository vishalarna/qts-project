using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class AddSkillQualificationEmpSettingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SkillQualificationEmpSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillQualificationId = table.Column<int>(type: "int", nullable: false),
                    ReleaseToAllSingleSignOff = table.Column<bool>(type: "bit", nullable: false),
                    MultipleSignOff = table.Column<int>(type: "int", nullable: true),
                    ReleaseOnReleaseDate = table.Column<bool>(type: "bit", nullable: false),
                    ReleaseInSpecificOrder = table.Column<bool>(type: "bit", nullable: false),
                    ShowSkillSuggestions = table.Column<bool>(type: "bit", nullable: false),
                    ShowSkillQuestions = table.Column<bool>(type: "bit", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillQualificationEmpSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillQualificationEmpSettings_SkillQualifications_SkillQualificationId",
                        column: x => x.SkillQualificationId,
                        principalTable: "SkillQualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkillQualificationEmpSettings_SkillQualificationId",
                table: "SkillQualificationEmpSettings",
                column: "SkillQualificationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkillQualificationEmpSettings");
        }
    }
}
