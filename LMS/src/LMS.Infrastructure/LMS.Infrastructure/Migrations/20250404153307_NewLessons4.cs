using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewLessons4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Lesson_LessonsID",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Groups_GroupID",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Teachers_TeacherID",
                table: "Lesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson");

            migrationBuilder.RenameTable(
                name: "Lesson",
                newName: "Lessons");

            migrationBuilder.RenameIndex(
                name: "IX_Lesson_TeacherID",
                table: "Lessons",
                newName: "IX_Lessons_TeacherID");

            migrationBuilder.RenameIndex(
                name: "IX_Lesson_GroupID",
                table: "Lessons",
                newName: "IX_Lessons_GroupID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Lessons_LessonsID",
                table: "Attendances",
                column: "LessonsID",
                principalTable: "Lessons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Groups_GroupID",
                table: "Lessons",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Teachers_TeacherID",
                table: "Lessons",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Lessons_LessonsID",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Groups_GroupID",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Teachers_TeacherID",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.RenameTable(
                name: "Lessons",
                newName: "Lesson");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_TeacherID",
                table: "Lesson",
                newName: "IX_Lesson_TeacherID");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_GroupID",
                table: "Lesson",
                newName: "IX_Lesson_GroupID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Lesson_LessonsID",
                table: "Attendances",
                column: "LessonsID",
                principalTable: "Lesson",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Groups_GroupID",
                table: "Lesson",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Teachers_TeacherID",
                table: "Lesson",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
