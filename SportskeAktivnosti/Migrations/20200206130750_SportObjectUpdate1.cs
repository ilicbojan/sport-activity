using Microsoft.EntityFrameworkCore.Migrations;

namespace SportskeAktivnosti.Migrations
{
    public partial class SportObjectUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "SportObjects");

            migrationBuilder.AddColumn<string>(
                name: "Image1Path",
                table: "SportObjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image2Path",
                table: "SportObjects",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPayed",
                table: "SportObjects",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image1Path",
                table: "SportObjects");

            migrationBuilder.DropColumn(
                name: "Image2Path",
                table: "SportObjects");

            migrationBuilder.DropColumn(
                name: "IsPayed",
                table: "SportObjects");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "SportObjects",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
