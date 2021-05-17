using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningAssistant.Migrations.Dao
{
    public partial class ScheduleEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Auditory",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Auditory",
                table: "Lessons");
        }
    }
}
