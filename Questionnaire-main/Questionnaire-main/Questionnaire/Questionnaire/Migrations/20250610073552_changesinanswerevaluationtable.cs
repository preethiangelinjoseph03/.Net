using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questionnaire.Migrations
{
    /// <inheritdoc />
    public partial class changesinanswerevaluationtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerEvaluation_CandidateAnswers_CandidateAnswersId",
                table: "AnswerEvaluation");

            migrationBuilder.DropIndex(
                name: "IX_AnswerEvaluation_CandidateAnswersId",
                table: "AnswerEvaluation");

            migrationBuilder.DropColumn(
                name: "CandidateAnswersId",
                table: "AnswerEvaluation");

            migrationBuilder.RenameColumn(
                name: "CandidateanswerId",
                table: "AnswerEvaluation",
                newName: "CandidateAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerEvaluation_CandidateAnswerId",
                table: "AnswerEvaluation",
                column: "CandidateAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerEvaluation_CandidateAnswers_CandidateAnswerId",
                table: "AnswerEvaluation",
                column: "CandidateAnswerId",
                principalTable: "CandidateAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerEvaluation_CandidateAnswers_CandidateAnswerId",
                table: "AnswerEvaluation");

            migrationBuilder.DropIndex(
                name: "IX_AnswerEvaluation_CandidateAnswerId",
                table: "AnswerEvaluation");

            migrationBuilder.RenameColumn(
                name: "CandidateAnswerId",
                table: "AnswerEvaluation",
                newName: "CandidateanswerId");

            migrationBuilder.AddColumn<int>(
                name: "CandidateAnswersId",
                table: "AnswerEvaluation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AnswerEvaluation_CandidateAnswersId",
                table: "AnswerEvaluation",
                column: "CandidateAnswersId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerEvaluation_CandidateAnswers_CandidateAnswersId",
                table: "AnswerEvaluation",
                column: "CandidateAnswersId",
                principalTable: "CandidateAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
