using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questionnaire.Migrations
{
    /// <inheritdoc />
    public partial class changedevaluationtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerEvaluation_CandidateAnswers_CandidateAnswersId",
                table: "AnswerEvaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerEvaluation_Question_QuestionId",
                table: "AnswerEvaluation");

            migrationBuilder.DropIndex(
                name: "IX_AnswerEvaluation_QuestionId",
                table: "AnswerEvaluation");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "AnswerEvaluation",
                newName: "Questiontext");

            migrationBuilder.RenameColumn(
                name: "CandidateAnswersId",
                table: "AnswerEvaluation",
                newName: "QuestionrefId");

            migrationBuilder.RenameColumn(
                name: "CandidateAnswerId",
                table: "AnswerEvaluation",
                newName: "CandidateAnswerText");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerEvaluation_CandidateAnswersId",
                table: "AnswerEvaluation",
                newName: "IX_AnswerEvaluation_QuestionrefId");

            migrationBuilder.AddColumn<int>(
                name: "CandidateAnswersrefId",
                table: "AnswerEvaluation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LanguagerefId",
                table: "AnswerEvaluation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "candidateName",
                table: "AnswerEvaluation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "languageName",
                table: "AnswerEvaluation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnswerEvaluation_CandidateAnswersrefId",
                table: "AnswerEvaluation",
                column: "CandidateAnswersrefId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerEvaluation_LanguagerefId",
                table: "AnswerEvaluation",
                column: "LanguagerefId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerEvaluation_CandidateAnswers_CandidateAnswersrefId",
                table: "AnswerEvaluation",
                column: "CandidateAnswersrefId",
                principalTable: "CandidateAnswers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerEvaluation_Languages_LanguagerefId",
                table: "AnswerEvaluation",
                column: "LanguagerefId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerEvaluation_Question_QuestionrefId",
                table: "AnswerEvaluation",
                column: "QuestionrefId",
                principalTable: "Question",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerEvaluation_CandidateAnswers_CandidateAnswersrefId",
                table: "AnswerEvaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerEvaluation_Languages_LanguagerefId",
                table: "AnswerEvaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerEvaluation_Question_QuestionrefId",
                table: "AnswerEvaluation");

            migrationBuilder.DropIndex(
                name: "IX_AnswerEvaluation_CandidateAnswersrefId",
                table: "AnswerEvaluation");

            migrationBuilder.DropIndex(
                name: "IX_AnswerEvaluation_LanguagerefId",
                table: "AnswerEvaluation");

            migrationBuilder.DropColumn(
                name: "CandidateAnswersrefId",
                table: "AnswerEvaluation");

            migrationBuilder.DropColumn(
                name: "LanguagerefId",
                table: "AnswerEvaluation");

            migrationBuilder.DropColumn(
                name: "candidateName",
                table: "AnswerEvaluation");

            migrationBuilder.DropColumn(
                name: "languageName",
                table: "AnswerEvaluation");

            migrationBuilder.RenameColumn(
                name: "Questiontext",
                table: "AnswerEvaluation",
                newName: "QuestionId");

            migrationBuilder.RenameColumn(
                name: "QuestionrefId",
                table: "AnswerEvaluation",
                newName: "CandidateAnswersId");

            migrationBuilder.RenameColumn(
                name: "CandidateAnswerText",
                table: "AnswerEvaluation",
                newName: "CandidateAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerEvaluation_QuestionrefId",
                table: "AnswerEvaluation",
                newName: "IX_AnswerEvaluation_CandidateAnswersId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerEvaluation_QuestionId",
                table: "AnswerEvaluation",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerEvaluation_CandidateAnswers_CandidateAnswersId",
                table: "AnswerEvaluation",
                column: "CandidateAnswersId",
                principalTable: "CandidateAnswers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerEvaluation_Question_QuestionId",
                table: "AnswerEvaluation",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
