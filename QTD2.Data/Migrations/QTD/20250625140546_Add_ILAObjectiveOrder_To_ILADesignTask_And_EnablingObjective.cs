using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTD2.Data.Migrations.QTD
{
    /// <inheritdoc />
    public partial class Add_ILAObjectiveOrder_To_ILADesignTask_And_EnablingObjective : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ILAObjectiveOrder",
                table: "InstructorWorkbook_ILADesign_Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ILAObjectiveOrder",
                table: "InstructorWorkbook_ILADesign_EnablingObjectives",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ILAObjectiveOrder",
                table: "InstructorWorkbook_ILADesign_Tasks");

            migrationBuilder.DropColumn(
                name: "ILAObjectiveOrder",
                table: "InstructorWorkbook_ILADesign_EnablingObjectives");
        }
    }
}
