using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class Add_ClassScheduleEmployee_ILACertification_PartialCredit_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassScheduleEmployee_ILACertificationLink_PartialCredits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassScheduleEmployeeId = table.Column<int>(type: "int", nullable: false),
                    ILACertificationLinkId = table.Column<int>(type: "int", nullable: false),
                    PartialCreditHours = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassScheduleEmployee_ILACertificationLink_PartialCredits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassScheduleEmployee_ILACertificationLink_PartialCredits_ClassScheduleEmployees_ClassScheduleEmployeeId",
                        column: x => x.ClassScheduleEmployeeId,
                        principalTable: "ClassScheduleEmployees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassScheduleEmployee_ILACertificationLink_PartialCredits_ILACertificationLinks_ILACertificationLinkId",
                        column: x => x.ILACertificationLinkId,
                        principalTable: "ILACertificationLinks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassScheduleEmployee_ILACertificationLink_PartialCreditId = table.Column<int>(type: "int", nullable: false),
                    ILACertificationSubRequirementLinkId = table.Column<int>(type: "int", nullable: false),
                    PartialCreditHours = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits_ClassScheduleEmployee_ILACertificationLink_PartialC~",
                        column: x => x.ClassScheduleEmployee_ILACertificationLink_PartialCreditId,
                        principalTable: "ClassScheduleEmployee_ILACertificationLink_PartialCredits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits_ILACertificationSubRequirementLinks_ILACertificatio~",
                        column: x => x.ILACertificationSubRequirementLinkId,
                        principalTable: "ILACertificationSubRequirementLinks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleEmployee_ILACertificationLink_PartialCredits_ClassScheduleEmployeeId",
                table: "ClassScheduleEmployee_ILACertificationLink_PartialCredits",
                column: "ClassScheduleEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleEmployee_ILACertificationLink_PartialCredits_ILACertificationLinkId",
                table: "ClassScheduleEmployee_ILACertificationLink_PartialCredits",
                column: "ILACertificationLinkId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits_ClassScheduleEmployee_ILACertificationLink_PartialC~",
                table: "ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits",
                column: "ClassScheduleEmployee_ILACertificationLink_PartialCreditId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits_ILACertificationSubRequirementLinkId",
                table: "ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits",
                column: "ILACertificationSubRequirementLinkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits");

            migrationBuilder.DropTable(
                name: "ClassScheduleEmployee_ILACertificationLink_PartialCredits");
        }
    }
}
