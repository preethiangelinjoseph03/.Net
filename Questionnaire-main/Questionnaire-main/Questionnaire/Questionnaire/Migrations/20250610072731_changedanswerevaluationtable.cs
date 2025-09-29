using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questionnaire.Migrations
{
    /// <inheritdoc />
    public partial class changedanswerevaluationtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerEvaluation_CandidateAnswers_CandidateAnswersId",
                table: "AnswerEvaluation");

            migrationBuilder.AlterColumn<int>(
                name: "CandidateAnswersId",
                table: "AnswerEvaluation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerEvaluation_CandidateAnswers_CandidateAnswersId",
                table: "AnswerEvaluation",
                column: "CandidateAnswersId",
                principalTable: "CandidateAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerEvaluation_CandidateAnswers_CandidateAnswersId",
                table: "AnswerEvaluation");

            migrationBuilder.AlterColumn<int>(
                name: "CandidateAnswersId",
                table: "AnswerEvaluation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerEvaluation_CandidateAnswers_CandidateAnswersId",
                table: "AnswerEvaluation",
                column: "CandidateAnswersId",
                principalTable: "CandidateAnswers",
                principalColumn: "Id");
        }
    }
}
