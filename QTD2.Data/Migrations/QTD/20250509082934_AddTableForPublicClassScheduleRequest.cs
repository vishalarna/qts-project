using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class AddTableForPublicClassScheduleRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublicClassScheduleRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassScheduleId = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NercCertNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertificationExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NercCertificationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClassScheduleEmployeeId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicClassScheduleRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicClassScheduleRequests_ClassScheduleEmployees_ClassScheduleEmployeeId",
                        column: x => x.ClassScheduleEmployeeId,
                        principalTable: "ClassScheduleEmployees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PublicClassScheduleRequests_ClassSchedules_ClassScheduleId",
                        column: x => x.ClassScheduleId,
                        principalTable: "ClassSchedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicClassScheduleRequests_ClassScheduleEmployeeId",
                table: "PublicClassScheduleRequests",
                column: "ClassScheduleEmployeeId",
                unique: true,
                filter: "[ClassScheduleEmployeeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PublicClassScheduleRequests_ClassScheduleId",
                table: "PublicClassScheduleRequests",
                column: "ClassScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicClassScheduleRequests");
        }
    }
}
