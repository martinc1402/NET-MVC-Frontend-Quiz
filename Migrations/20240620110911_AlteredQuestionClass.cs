using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontEndQuiz.Migrations
{
    /// <inheritdoc />
    public partial class AlteredQuestionClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "Questions");
        }
    }
}
