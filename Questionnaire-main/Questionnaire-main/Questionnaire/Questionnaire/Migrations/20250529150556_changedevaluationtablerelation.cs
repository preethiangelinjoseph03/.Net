using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questionnaire.Migrations
{
    /// <inheritdoc />
    public partial class changedevaluationtablerelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerEvaluation_AspNetUsers_CandidateId",
                table: "AnswerEvaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerEvaluation_Languages_LanguagerefId",
                table: "AnswerEvaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerEvaluation_Question_QuestionrefId",
                table: "AnswerEvaluation");

            migrationBuilder.DropIndex(
                name: "IX_AnswerEvaluation_CandidateId",
                table: "AnswerEvaluation");

            migrationBuilder.DropIndex(
                name: "IX_AnswerEvaluation_LanguagerefId",
                table: "AnswerEvaluation");

            migrationBuilder.DropIndex(
                name: "IX_AnswerEvaluation_QuestionrefId",
                table: "AnswerEvaluation");

            migrationBuilder.DropColumn(
                name: "CandidateAnswerText",
                table: "AnswerEvaluation");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "AnswerEvaluation");

            migrationBuilder.DropColumn(
                name: "LanguagerefId",
                table: "AnswerEvaluation");

            migrationBuilder.DropColumn(
                name: "QuestionrefId",
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
                newName: "CandidateAnswerId");

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "CandidateAnswers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateAnswers_LanguageId",
                table: "CandidateAnswers",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateAnswers_Languages_LanguageId",
                table: "CandidateAnswers",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateAnswers_Languages_LanguageId",
                table: "CandidateAnswers");

            migrationBuilder.DropIndex(
                name: "IX_CandidateAnswers_LanguageId",
                table: "CandidateAnswers");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "CandidateAnswers");

            migrationBuilder.RenameColumn(
                name: "CandidateAnswerId",
                table: "AnswerEvaluation",
                newName: "Questiontext");

            migrationBuilder.AddColumn<int>(
                name: "CandidateAnswerText",
                table: "AnswerEvaluation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CandidateId",
                table: "AnswerEvaluation",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LanguagerefId",
                table: "AnswerEvaluation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionrefId",
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
                name: "IX_AnswerEvaluation_CandidateId",
                table: "AnswerEvaluation",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerEvaluation_LanguagerefId",
                table: "AnswerEvaluation",
                column: "LanguagerefId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerEvaluation_QuestionrefId",
                table: "AnswerEvaluation",
                column: "QuestionrefId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerEvaluation_AspNetUsers_CandidateId",
                table: "AnswerEvaluation",
                column: "CandidateId",
                principalTable: "AspNetUsers",
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
    }
}
