using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontEndQuiz.Migrations
{
    /// <inheritdoc />
    public partial class ChangedQuestionDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditedQuestionString",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "EditedTitle",
                table: "Questions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EditedQuestionString",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EditedTitle",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
