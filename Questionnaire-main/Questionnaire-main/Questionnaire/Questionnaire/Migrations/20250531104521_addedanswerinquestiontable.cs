using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questionnaire.Migrations
{
    /// <inheritdoc />
    public partial class addedanswerinquestiontable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answertext",
                table: "OptionsAndAnswer");

            migrationBuilder.AddColumn<string>(
                name: "Answertext",
                table: "Question",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answertext",
                table: "Question");

            migrationBuilder.AddColumn<string>(
                name: "Answertext",
                table: "OptionsAndAnswer",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
