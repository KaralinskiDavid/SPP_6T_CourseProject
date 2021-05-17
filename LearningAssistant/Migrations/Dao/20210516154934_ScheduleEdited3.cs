using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningAssistant.Migrations.Dao
{
    public partial class ScheduleEdited3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaySchedules_Shcedules_ScheduleId",
                table: "DaySchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Shcedules_Groups_GroupId",
                table: "Shcedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shcedules",
                table: "Shcedules");

            migrationBuilder.RenameTable(
                name: "Shcedules",
                newName: "Schedules");

            migrationBuilder.RenameIndex(
                name: "IX_Shcedules_GroupId",
                table: "Schedules",
                newName: "IX_Schedules_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DaySchedules_Schedules_ScheduleId",
                table: "DaySchedules",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Groups_GroupId",
                table: "Schedules",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaySchedules_Schedules_ScheduleId",
                table: "DaySchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Groups_GroupId",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.RenameTable(
                name: "Schedules",
                newName: "Shcedules");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_GroupId",
                table: "Shcedules",
                newName: "IX_Shcedules_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shcedules",
                table: "Shcedules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DaySchedules_Shcedules_ScheduleId",
                table: "DaySchedules",
                column: "ScheduleId",
                principalTable: "Shcedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shcedules_Groups_GroupId",
                table: "Shcedules",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
