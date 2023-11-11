using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataVillasTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "wifi", new DateTime(2023, 11, 11, 13, 26, 44, 135, DateTimeKind.Local).AddTicks(6593), "Some dummy meanighless text", "https://images.unsplash.com/photo-1679678690998-88c8711cbe5f?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDF8MHxzZWFyY2h8MXx8aG90ZWx8ZW58MHx8MHx8fDA%3D", "Aarya inn", 5, 2500.0, 200, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "parking", new DateTime(2023, 11, 11, 13, 26, 44, 135, DateTimeKind.Local).AddTicks(6608), "Some dummy meanighless text", "https://plus.unsplash.com/premium_photo-1678240508014-d1ab7345bfe6?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NXx8aG90ZWx8ZW58MHx8MHx8fDA%3D", "People hotel", 15, 500.0, 200, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "all", new DateTime(2023, 11, 11, 13, 26, 44, 135, DateTimeKind.Local).AddTicks(6611), "Some dummy meanighless text", "https://plus.unsplash.com/premium_photo-1675745329954-9639d3b74bbf?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8aG90ZWx8ZW58MHx8MHx8fDA%3D", "Mriott", 55, 8500.0, 9870, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
