using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace interval_recall.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionQuestionGroupId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Qestions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_DecisionQualities_Qestions_QuestionId",
                table: "DecisionQualities");

            migrationBuilder.DropForeignKey(
                name: "FK_Qestions_QuestionGroups_QuestionGroupId",
                table: "Qestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Qestions",
                table: "Qestions");

            migrationBuilder.RenameTable(
                name: "Qestions",
                newName: "Questions");

            migrationBuilder.RenameIndex(
                name: "IX_Qestions_QuestionGroupId",
                table: "Questions",
                newName: "IX_Questions_QuestionGroupId");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionGroupId",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DecisionQualities_Questions_QuestionId",
                table: "DecisionQualities",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuestionGroups_QuestionGroupId",
                table: "Questions",
                column: "QuestionGroupId",
                principalTable: "QuestionGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_DecisionQualities_Questions_QuestionId",
                table: "DecisionQualities");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuestionGroups_QuestionGroupId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Qestions");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_QuestionGroupId",
                table: "Qestions",
                newName: "IX_Qestions_QuestionGroupId");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionGroupId",
                table: "Qestions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Qestions",
                table: "Qestions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Qestions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Qestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DecisionQualities_Qestions_QuestionId",
                table: "DecisionQualities",
                column: "QuestionId",
                principalTable: "Qestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Qestions_QuestionGroups_QuestionGroupId",
                table: "Qestions",
                column: "QuestionGroupId",
                principalTable: "QuestionGroups",
                principalColumn: "Id");
        }
    }
}
