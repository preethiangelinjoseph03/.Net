using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questionnaire.Migrations
{
    /// <inheritdoc />
    public partial class addedordertocandidatanswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateAnswers_AspNetUsers_CandidateId",
                table: "CandidateAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateAnswers_OptionsAndAnswer_OptionsAndAnswerId",
                table: "CandidateAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_AspNetUsers_UserId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_UserId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_CandidateAnswers_CandidateId",
                table: "CandidateAnswers");

            migrationBuilder.DropIndex(
                name: "IX_CandidateAnswers_OptionsAndAnswerId",
                table: "CandidateAnswers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "CandidateAnswers");

            migrationBuilder.DropColumn(
                name: "OptionsAndAnswerId",
                table: "CandidateAnswers");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CandidateAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "CandidateAnswers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Question",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CandidateId",
                table: "CandidateAnswers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OptionsAndAnswerId",
                table: "CandidateAnswers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Question_UserId",
                table: "Question",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateAnswers_CandidateId",
                table: "CandidateAnswers",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateAnswers_OptionsAndAnswerId",
                table: "CandidateAnswers",
                column: "OptionsAndAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateAnswers_AspNetUsers_CandidateId",
                table: "CandidateAnswers",
                column: "CandidateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateAnswers_OptionsAndAnswer_OptionsAndAnswerId",
                table: "CandidateAnswers",
                column: "OptionsAndAnswerId",
                principalTable: "OptionsAndAnswer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_AspNetUsers_UserId",
                table: "Question",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
