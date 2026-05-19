using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLineItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(1500)", unicode: false, maxLength: 1500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LineItemType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItemType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "LineItemType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Gas" },
                    { 1, "Groceries" },
                    { 2, "Fast Food" },
                    { 3, "Entertainment" },
                    { 4, "Bills" },
                    { 5, "Cash" },
                    { 6, "Shopping" },
                    { 7, "Gifts" },
                    { 8, "Automotive" },
                    { 9, "Health" },
                    { 10, "Home" },
                    { 11, "Travel" },
                    { 12, "Professional Services" },
                    { 13, "Education" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineItems");

            migrationBuilder.DropTable(
                name: "LineItemType");
        }
    }
}
