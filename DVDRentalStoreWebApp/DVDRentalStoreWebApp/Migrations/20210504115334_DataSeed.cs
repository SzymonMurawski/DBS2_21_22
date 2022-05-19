using Microsoft.EntityFrameworkCore.Migrations;

namespace DVDRentalStoreWebApp.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Price", "Title", "Year" },
                values: new object[,]
                {
                    { 1, 12.0, "Anchorman", 2000 },
                    { 2, 7.0, "Anchorman 2", 2001 },
                    { 3, 22.0, "Terminator", 1993 },
                    { 4, 29.0, "Jurassic Park", 1999 },
                    { 5, 11.0, "The Lord of the Rings", 1997 }
                });

            migrationBuilder.InsertData(
                table: "Copies",
                columns: new[] { "Id", "Available", "MovieId" },
                values: new object[,]
                {
                    { 1, true, 1 },
                    { 2, true, 1 },
                    { 3, false, 1 },
                    { 4, true, 3 },
                    { 5, true, 3 },
                    { 6, true, 5 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Copies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Copies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Copies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Copies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Copies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Copies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
