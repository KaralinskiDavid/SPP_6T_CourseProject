using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningAssistant.Migrations.Dao
{
    public partial class ss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecialityId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeadStudentId",
                table: "Specialities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialities_HeadStudentId",
                table: "Specialities",
                column: "HeadStudentId",
                unique: true,
                filter: "[HeadStudentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Specialities_Students_HeadStudentId",
                table: "Specialities",
                column: "HeadStudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specialities_Students_HeadStudentId",
                table: "Specialities");

            migrationBuilder.DropIndex(
                name: "IX_Specialities_HeadStudentId",
                table: "Specialities");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "HeadStudentId",
                table: "Specialities");
        }
    }
}
