using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CsvUpload.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Application");

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeterReadings",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ReadingTaken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeterReadings", x => new { x.Id, x.ReadingTaken });
                    table.ForeignKey(
                        name: "FK_MeterReadings_Accounts_Id",
                        column: x => x.Id,
                        principalSchema: "Application",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeterReadings",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "Application");
        }
    }
}
