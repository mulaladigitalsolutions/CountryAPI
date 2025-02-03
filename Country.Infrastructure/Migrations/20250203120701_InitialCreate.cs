using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Country.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Population = table.Column<long>(type: "bigint", nullable: false),
                    Capital = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Capital", "Flag", "Name", "Population" },
                values: new object[,]
                {
                    { 1, "Washington, D.C.", "🇺🇸", "USA", 331002651L },
                    { 2, "Ottawa", "🇨🇦", "Canada", 37742154L },
                    { 3, "Pretoria", "🇿🇦", "South Africa", 59308690L },
                    { 4, "Windhoek", "🇳🇦", "Namibia", 2540905L },
                    { 5, "Kigali", "🇷🇼", "Rwanda", 12952218L },
                    { 6, "Berlin", "🇩🇪", "Germany", 83166711L },
                    { 7, "Paris", "🇫🇷", "France", 65273511L },
                    { 8, "Tokyo", "🇯🇵", "Japan", 126476461L },
                    { 9, "Brasília", "🇧🇷", "Brazil", 212559417L },
                    { 10, "Canberra", "🇦🇺", "Australia", 25687041L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
