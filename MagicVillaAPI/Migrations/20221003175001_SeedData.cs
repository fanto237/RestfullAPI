using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVillaAPI.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Surface", "UpdatedDate" },
                values: new object[] { 1, "Sed facilisis, nibh id aliquet ultrices, ante nisi egestas turpis, euismod ornare metus urna eu neque. Etiam scelerisque elit rutrum augue sollicitudin sollicitudin.", new DateTime(2022, 10, 3, 19, 50, 1, 411, DateTimeKind.Local).AddTicks(2492), "Cras ut nunc at leo vehicula gravida sit amet sed odio. Suspendisse placerat porta urna in elementum", "https://images.unsplash.com/photo-1600585154340-be6161a56a0c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80", "Chateau d'eau", 4, 12.0, 300, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Surface", "UpdatedDate" },
                values: new object[] { 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean ut tincidunt ipsum. Morbi ex diam, euismod et erat viverra, aliquam malesuada risus. Etiam viverra sed elit non tempor", new DateTime(2022, 10, 3, 19, 50, 1, 411, DateTimeKind.Local).AddTicks(2515), "Mauris eget eros velit. Sed non facilisis risus, non cursus diam. Aenean gravida ac ex vitae malesuada. Integer venenatis interdum odio ut sodales.", "https://images.unsplash.com/photo-1600585154340-be6161a56a0c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80", "Mon Village", 4, 12.0, 500, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

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
        }
    }
}
