using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class AddEmailNotificationForPublicClassScheduleRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublicClassScheduleRequestAcceptedNotification_PublicClassScheduleRequestId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublicClassScheduleRequestNotification_PublicClassScheduleRequestId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PublicClassScheduleRequestAcceptedNotification_PublicClassScheduleRequestId",
                table: "Notifications",
                column: "PublicClassScheduleRequestAcceptedNotification_PublicClassScheduleRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PublicClassScheduleRequestNotification_PublicClassScheduleRequestId",
                table: "Notifications",
                column: "PublicClassScheduleRequestNotification_PublicClassScheduleRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_PublicClassScheduleRequests_PublicClassScheduleRequestAcceptedNotification_PublicClassScheduleRequestId",
                table: "Notifications",
                column: "PublicClassScheduleRequestAcceptedNotification_PublicClassScheduleRequestId",
                principalTable: "PublicClassScheduleRequests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_PublicClassScheduleRequests_PublicClassScheduleRequestNotification_PublicClassScheduleRequestId",
                table: "Notifications",
                column: "PublicClassScheduleRequestNotification_PublicClassScheduleRequestId",
                principalTable: "PublicClassScheduleRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_PublicClassScheduleRequests_PublicClassScheduleRequestAcceptedNotification_PublicClassScheduleRequestId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_PublicClassScheduleRequests_PublicClassScheduleRequestNotification_PublicClassScheduleRequestId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_PublicClassScheduleRequestAcceptedNotification_PublicClassScheduleRequestId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_PublicClassScheduleRequestNotification_PublicClassScheduleRequestId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "PublicClassScheduleRequestAcceptedNotification_PublicClassScheduleRequestId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "PublicClassScheduleRequestNotification_PublicClassScheduleRequestId",
                table: "Notifications");
        }
    }
}
