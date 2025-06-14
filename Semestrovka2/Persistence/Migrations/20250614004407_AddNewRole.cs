using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1b5ec9fc-e1d6-4929-8083-5248025bf7ce"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e8c0251c-81e3-452d-b4bb-26f3de39305d"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("763dea19-4ceb-4e3b-8a8d-4774d57ed153"), null, "Owner", "OWNER" },
                    { new Guid("c016751d-ce56-4b8f-a851-f7155188ebef"), null, "User", "USER" },
                    { new Guid("fc30c994-f306-4748-85f6-dd6e2046a63e"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("763dea19-4ceb-4e3b-8a8d-4774d57ed153"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c016751d-ce56-4b8f-a851-f7155188ebef"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fc30c994-f306-4748-85f6-dd6e2046a63e"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1b5ec9fc-e1d6-4929-8083-5248025bf7ce"), null, "Admin", "ADMIN" },
                    { new Guid("e8c0251c-81e3-452d-b4bb-26f3de39305d"), null, "User", "USER" }
                });
        }
    }
}
