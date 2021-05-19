using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningAssistant.Migrations.Dao
{
    public partial class Files : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpecialityFileSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialityId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialityFileSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialityFileSections_Specialities_SpecialityId",
                        column: x => x.SpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialityFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialityFileSectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialityFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialityFiles_SpecialityFileSections_SpecialityFileSectionId",
                        column: x => x.SpecialityFileSectionId,
                        principalTable: "SpecialityFileSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecialityFiles_SpecialityFileSectionId",
                table: "SpecialityFiles",
                column: "SpecialityFileSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialityFileSections_SpecialityId",
                table: "SpecialityFileSections",
                column: "SpecialityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecialityFiles");

            migrationBuilder.DropTable(
                name: "SpecialityFileSections");
        }
    }
}
