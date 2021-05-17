using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningAssistant.Migrations.Dao
{
    public partial class ScheduleEdited2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shcedules_GroupId",
                table: "Shcedules");

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "Groups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shcedules_GroupId",
                table: "Shcedules",
                column: "GroupId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shcedules_GroupId",
                table: "Shcedules");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Groups");

            migrationBuilder.CreateIndex(
                name: "IX_Shcedules_GroupId",
                table: "Shcedules",
                column: "GroupId");
        }
    }
}
