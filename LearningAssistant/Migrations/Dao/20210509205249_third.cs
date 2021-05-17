using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningAssistant.Migrations.Dao
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubGroup",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Shcedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DaySchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaySchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DaySchedules_Shcedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Shcedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayScheduleId = table.Column<int>(type: "int", nullable: false),
                    LessonTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubGroup = table.Column<int>(type: "int", nullable: false),
                    LessonTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_DaySchedules_DayScheduleId",
                        column: x => x.DayScheduleId,
                        principalTable: "DaySchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lessons_LessonTypes_LessonTypeId",
                        column: x => x.LessonTypeId,
                        principalTable: "LessonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shcedules_GroupId",
                table: "Shcedules",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DaySchedules_ScheduleId",
                table: "DaySchedules",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_DayScheduleId",
                table: "Lessons",
                column: "DayScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_LessonTypeId",
                table: "Lessons",
                column: "LessonTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shcedules_Groups_GroupId",
                table: "Shcedules",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shcedules_Groups_GroupId",
                table: "Shcedules");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "DaySchedules");

            migrationBuilder.DropTable(
                name: "LessonTypes");

            migrationBuilder.DropIndex(
                name: "IX_Shcedules_GroupId",
                table: "Shcedules");

            migrationBuilder.DropColumn(
                name: "SubGroup",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Shcedules");
        }
    }
}
