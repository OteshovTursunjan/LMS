using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewLessons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Subjectid = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjcetID = table.Column<Guid>(type: "uuid", nullable: false),
                    TeacherID = table.Column<Guid>(type: "uuid", nullable: false),
                    LessonTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Room = table.Column<string>(type: "text", nullable: false),
                    GroupID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateBY = table.Column<string>(type: "text", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.id);
                    table.ForeignKey(
                        name: "FK_Lesson_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lesson_Subjects_Subjectid",
                        column: x => x.Subjectid,
                        principalTable: "Subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lesson_Teachers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Teachers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_GroupID",
                table: "Lesson",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_Subjectid",
                table: "Lesson",
                column: "Subjectid");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_TeacherID",
                table: "Lesson",
                column: "TeacherID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lesson");
        }
    }
}
