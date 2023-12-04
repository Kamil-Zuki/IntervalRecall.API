using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace interval_recall.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddModel_QuestionGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Qestions_QuestionGroup_QuestionGroupId",
                table: "Qestions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionGroup_User_UserId",
                table: "QuestionGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionGroup",
                table: "QuestionGroup");

            migrationBuilder.RenameTable(
                name: "QuestionGroup",
                newName: "QestionGroups");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionGroup_UserId",
                table: "QestionGroups",
                newName: "IX_QestionGroups_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QestionGroups",
                table: "QestionGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QestionGroups_User_UserId",
                table: "QestionGroups",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Qestions_QestionGroups_QuestionGroupId",
                table: "Qestions",
                column: "QuestionGroupId",
                principalTable: "QestionGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QestionGroups_User_UserId",
                table: "QestionGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Qestions_QestionGroups_QuestionGroupId",
                table: "Qestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QestionGroups",
                table: "QestionGroups");

            migrationBuilder.RenameTable(
                name: "QestionGroups",
                newName: "QuestionGroup");

            migrationBuilder.RenameIndex(
                name: "IX_QestionGroups_UserId",
                table: "QuestionGroup",
                newName: "IX_QuestionGroup_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionGroup",
                table: "QuestionGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Qestions_QuestionGroup_QuestionGroupId",
                table: "Qestions",
                column: "QuestionGroupId",
                principalTable: "QuestionGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionGroup_User_UserId",
                table: "QuestionGroup",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
