using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BondTrades.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bonds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Isin = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Issuer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CouponRate = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    MaturityDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonds", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Bonds",
                columns: new[] { "Id", "CouponRate", "Currency", "Isin", "Issuer", "MaturityDate" },
                values: new object[,]
                {
                    { 1, 4.25m, "USD", "US1234567890", "Example Corp", new DateOnly(2030, 6, 15) },
                    { 2, 5.1m, "GBP", "GB1234567890", "Example Bank", new DateOnly(2032, 3, 20) },
                    { 3, 6.25m, "USD", "US1324567890", "Example Energy Corp", new DateOnly(2030, 6, 20) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bonds_Isin",
                table: "Bonds",
                column: "Isin",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bonds");
        }
    }
}
