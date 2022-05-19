using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DVDRentalStoreWebApp.Migrations
{
    public partial class TablePerHierarchy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true),
                    HireDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Birthday", "Discriminator", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 2, new DateTime(1977, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Client", "Bob", "Belcher" },
                    { 3, new DateTime(1954, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Client", "Hank", "Hill" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Discriminator", "FirstName", "HireDate", "LastName", "Location" },
                values: new object[] { 1, "Employee", "Jace", new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beleren", "Poznan" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
