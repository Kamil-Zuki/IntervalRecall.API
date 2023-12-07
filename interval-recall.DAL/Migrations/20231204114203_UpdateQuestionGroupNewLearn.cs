using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace interval_recall.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuestionGroupNewLearn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "New",
                table: "QuestionGroups",
                newName: "AmountOfNew");

            migrationBuilder.RenameColumn(
                name: "Learn",
                table: "QuestionGroups",
                newName: "AmountOfLearn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountOfNew",
                table: "QuestionGroups",
                newName: "New");

            migrationBuilder.RenameColumn(
                name: "AmountOfLearn",
                table: "QuestionGroups",
                newName: "Learn");
        }
    }
}
