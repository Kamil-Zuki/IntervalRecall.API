using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace interval_recall.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AlterEntityQuestionInterval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interval",
                table: "Questions");

            migrationBuilder.AddColumn<long>(
                name: "IntervalTicks",
                table: "Questions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntervalTicks",
                table: "Questions");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Interval",
                table: "Questions",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
