using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportskeAktivnosti.Migrations
{
    public partial class ValidationInitial11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WorkStarts",
                table: "SportObjects",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<string>(
                name: "WorkEnds",
                table: "SportObjects",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "WorkStarts",
                table: "SportObjects",
                type: "time",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "WorkEnds",
                table: "SportObjects",
                type: "time",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
