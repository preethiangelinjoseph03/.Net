using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questionnaire.Migrations
{
    /// <inheritdoc />
    public partial class addedanswerinoptionandanswertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerEvaluation_CandidateAnswers_CandidateAnswersrefId",
                table: "AnswerEvaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateAnswers_Languages_LanguageId",
                table: "CandidateAnswers");

            migrationBuilder.RenameColumn(
                name: "CandidateAnswerId",
                table: "AnswerEvaluation",
                newName: "CandidateanswerId");

            migrationBuilder.RenameColumn(
                name: "CandidateAnswersrefId",
                table: "AnswerEvaluation",
                newName: "CandidateAnswersId");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerEvaluation_CandidateAnswersrefId",
                table: "AnswerEvaluation",
                newName: "IX_AnswerEvaluation_CandidateAnswersId");

            migrationBuilder.AddColumn<string>(
                name: "Answertext",
                table: "OptionsAndAnswer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "CandidateAnswers",
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
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateAnswers_Languages_LanguageId",
                table: "CandidateAnswers",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerEvaluation_CandidateAnswers_CandidateAnswersId",
                table: "AnswerEvaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateAnswers_Languages_LanguageId",
                table: "CandidateAnswers");

            migrationBuilder.DropColumn(
                name: "Answertext",
                table: "OptionsAndAnswer");

            migrationBuilder.RenameColumn(
                name: "CandidateanswerId",
                table: "AnswerEvaluation",
                newName: "CandidateAnswerId");

            migrationBuilder.RenameColumn(
                name: "CandidateAnswersId",
                table: "AnswerEvaluation",
                newName: "CandidateAnswersrefId");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerEvaluation_CandidateAnswersId",
                table: "AnswerEvaluation",
                newName: "IX_AnswerEvaluation_CandidateAnswersrefId");

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "CandidateAnswers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerEvaluation_CandidateAnswers_CandidateAnswersrefId",
                table: "AnswerEvaluation",
                column: "CandidateAnswersrefId",
                principalTable: "CandidateAnswers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateAnswers_Languages_LanguageId",
                table: "CandidateAnswers",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");
        }
    }
}
