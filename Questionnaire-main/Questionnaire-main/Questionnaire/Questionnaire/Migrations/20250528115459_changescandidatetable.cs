using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questionnaire.Migrations
{
    /// <inheritdoc />
    public partial class changescandidatetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CandidateId",
                table: "CandidateAnswers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateAnswers_CandidateId",
                table: "CandidateAnswers",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateAnswers_AspNetUsers_CandidateId",
                table: "CandidateAnswers",
                column: "CandidateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateAnswers_AspNetUsers_CandidateId",
                table: "CandidateAnswers");

            migrationBuilder.DropIndex(
                name: "IX_CandidateAnswers_CandidateId",
                table: "CandidateAnswers");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "CandidateAnswers");
        }
    }
}
