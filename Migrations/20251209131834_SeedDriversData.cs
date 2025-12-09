using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace f1api.Migrations
{
    /// <inheritdoc />
    public partial class SeedDriversData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "Description", "Name", "RacingNumber", "TeamId" },
                values: new object[,]
                {
                    { 1, "", "Max Verstappen", 33, 1 },
                    { 2, "", "Sergio Perez", 11, 1 },
                    { 3, "", "George Russell", 63, 2 },
                    { 4, "", "Kimi Antonelli", 12, 2 },
                    { 5, "", "Charles Leclerc", 16, 3 },
                    { 6, "", "Lewis Hamilton", 44, 3 },
                    { 7, "", "Lando Norris", 4, 4 },
                    { 8, "", "Oscar Piastri", 81, 4 },
                    { 9, "", "Fernando Alonso", 14, 5 },
                    { 10, "", "Lance Stroll", 18, 5 },
                    { 11, "", "Pierre Gasly", 10, 6 },
                    { 12, "", "Jack Doohan", 7, 6 },
                    { 13, "", "Alexander Albon", 23, 7 },
                    { 14, "", "Carlos Sainz", 55, 7 },
                    { 15, "", "Yuki Tsunoda", 22, 8 },
                    { 16, "", "Liam Lawson", 30, 8 },
                    { 17, "", "Nico Hulkenberg", 27, 9 },
                    { 18, "", "GabrielBortoleto", 5, 9 },
                    { 19, "", "Esteban Ocon", 31, 10 },
                    { 20, "", "Oliver Bearman", 87, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
