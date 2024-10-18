using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learning_World.Migrations
{
    /// <inheritdoc />
    public partial class updatelessoncompletetion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModuleId",
                table: "LessonCompletions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LessonCompletions_ModuleId",
                table: "LessonCompletions",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonCompletions_Modules_ModuleId",
                table: "LessonCompletions",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "ModuleID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonCompletions_Modules_ModuleId",
                table: "LessonCompletions");

            migrationBuilder.DropIndex(
                name: "IX_LessonCompletions_ModuleId",
                table: "LessonCompletions");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "LessonCompletions");
        }
    }
}
