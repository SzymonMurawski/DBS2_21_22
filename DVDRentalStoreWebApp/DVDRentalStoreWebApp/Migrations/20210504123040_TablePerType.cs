using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DVDRentalStoreWebApp.Migrations
{
    public partial class TablePerType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "People");

            migrationBuilder.DropColumn(
                name: "HireDate",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "People");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Birthday = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_People_Id",
                        column: x => x.Id,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Location = table.Column<string>(type: "text", nullable: true),
                    HireDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_People_Id",
                        column: x => x.Id,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Birthday" },
                values: new object[] { 2, new DateTime(1977, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Birthday" },
                values: new object[] { 3, new DateTime(1954, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "HireDate", "Location" },
                values: new object[] { 1, new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Poznan" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "People",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "People",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "HireDate",
                table: "People",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "People",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Discriminator", "FirstName", "HireDate", "LastName", "Location" },
                values: new object[] { 1, "Employee", "Jace", new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beleren", "Poznan" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Birthday", "Discriminator", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 3, new DateTime(1954, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Client", "Hank", "Hill" },
                    { 2, new DateTime(1977, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Client", "Bob", "Belcher" }
                });
        }
    }
}
