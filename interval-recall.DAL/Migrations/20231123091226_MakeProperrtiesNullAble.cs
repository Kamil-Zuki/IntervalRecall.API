using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace interval_recall.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MakeProperrtiesNullAble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QestionGroups_User_UserId",
                table: "QestionGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Qestions_QestionGroups_QuestionGroupId",
                table: "Qestions");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserGroups_UserGroupId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QestionGroups",
                table: "QestionGroups");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "QestionGroups",
                newName: "QuestionGroups");

            migrationBuilder.RenameIndex(
                name: "IX_User_UserGroupId",
                table: "Users",
                newName: "IX_Users_UserGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_QestionGroups_UserId",
                table: "QuestionGroups",
                newName: "IX_QuestionGroups_UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserGroupId",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "QuestionGroups",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionGroups",
                table: "QuestionGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Qestions_QuestionGroups_QuestionGroupId",
                table: "Qestions",
                column: "QuestionGroupId",
                principalTable: "QuestionGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionGroups_Users_UserId",
                table: "QuestionGroups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserGroups_UserGroupId",
                table: "Users",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Qestions_QuestionGroups_QuestionGroupId",
                table: "Qestions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionGroups_Users_UserId",
                table: "QuestionGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserGroups_UserGroupId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionGroups",
                table: "QuestionGroups");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "QuestionGroups",
                newName: "QestionGroups");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserGroupId",
                table: "User",
                newName: "IX_User_UserGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionGroups_UserId",
                table: "QestionGroups",
                newName: "IX_QestionGroups_UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserGroupId",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "QestionGroups",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserGroups_UserGroupId",
                table: "User",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
