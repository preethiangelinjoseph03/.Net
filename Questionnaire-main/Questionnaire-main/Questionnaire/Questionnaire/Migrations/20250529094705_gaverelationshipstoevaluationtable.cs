using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questionnaire.Migrations
{
    /// <inheritdoc />
    public partial class gaverelationshipstoevaluationtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CandidateId",
                table: "AnswerEvaluation",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CandidateAnswersId",
                table: "AnswerEvaluation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnswerEvaluation_CandidateAnswersId",
                table: "AnswerEvaluation",
                column: "CandidateAnswersId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerEvaluation_CandidateId",
                table: "AnswerEvaluation",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerEvaluation_QuestionId",
                table: "AnswerEvaluation",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerEvaluation_AspNetUsers_CandidateId",
                table: "AnswerEvaluation",
                column: "CandidateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerEvaluation_AspNetUsers_CandidateId",
                table: "AnswerEvaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerEvaluation_CandidateAnswers_CandidateAnswersId",
                table: "AnswerEvaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerEvaluation_Question_QuestionId",
                table: "AnswerEvaluation");

            migrationBuilder.DropIndex(
                name: "IX_AnswerEvaluation_CandidateAnswersId",
                table: "AnswerEvaluation");

            migrationBuilder.DropIndex(
                name: "IX_AnswerEvaluation_CandidateId",
                table: "AnswerEvaluation");

            migrationBuilder.DropIndex(
                name: "IX_AnswerEvaluation_QuestionId",
                table: "AnswerEvaluation");

            migrationBuilder.DropColumn(
                name: "CandidateAnswersId",
                table: "AnswerEvaluation");

            migrationBuilder.AlterColumn<string>(
                name: "CandidateId",
                table: "AnswerEvaluation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
