using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportskeAktivnosti.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SportObjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    PriceForHour = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: false),
                    WorkStarts = table.Column<TimeSpan>(nullable: true),
                    WorkEnds = table.Column<TimeSpan>(nullable: true),
                    SportId = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SportObjects_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SportObjects_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SportSchools",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    SportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportSchools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SportSchools_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SportSchools_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PriceParticipation = table.Column<int>(nullable: false),
                    PricePerGame = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    SportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tournaments_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Srbija" },
                    { 2, "Hrvatska" },
                    { 3, "BiH" },
                    { 4, "Crna Gora" }
                });

            migrationBuilder.InsertData(
                table: "Sports",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fudbal" },
                    { 2, "Kosarka" },
                    { 3, "Tenis" },
                    { 4, "Teretana" },
                    { 5, "Bazen" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Beograd" },
                    { 2, 1, "Novi Sad" },
                    { 3, 2, "Zagreb" },
                    { 4, 3, "Sarajevo" },
                    { 5, 4, "Podgorica" }
                });

            migrationBuilder.InsertData(
                table: "SportObjects",
                columns: new[] { "Id", "Address", "CityId", "Description", "Email", "ImagePath", "Name", "Phone", "PriceForHour", "SportId", "WorkEnds", "WorkStarts" },
                values: new object[,]
                {
                    { 1, "test1 adresa", 1, null, "test1@test.com", null, "Test1", "0652001", 4000, 1, null, null },
                    { 2, "test2 adresa", 1, null, "test2@test.com", null, "Test2", "0652001", 4000, 1, null, null },
                    { 3, "test3 adresa", 1, null, "test3@test.com", null, "Test3", "0652001", 4000, 1, null, null },
                    { 4, "test4 adresa", 1, null, "test4@test.com", null, "Test4", "0652001", 4000, 2, null, null },
                    { 5, "test5 adresa", 1, null, "test5@test.com", null, "Test5", "0652001", 4000, 3, null, null },
                    { 6, "test6 adresa", 1, null, "test6@test.com", null, "Test6", "0652001", 4000, 4, null, null },
                    { 7, "test7 adresa", 1, null, "test7@test.com", null, "Test7", "0652001", 4000, 5, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_SportObjects_CityId",
                table: "SportObjects",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SportObjects_SportId",
                table: "SportObjects",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_SportSchools_CityId",
                table: "SportSchools",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SportSchools_SportId",
                table: "SportSchools",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_CityId",
                table: "Tournaments",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_SportId",
                table: "Tournaments",
                column: "SportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SportObjects");

            migrationBuilder.DropTable(
                name: "SportSchools");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Sports");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
