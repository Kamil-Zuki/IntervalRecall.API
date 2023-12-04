using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace interval_recall.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionStepAndState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Step",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Step",
                table: "Questions");
        }
    }
}
