using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFriendCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FriendCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FriendCategoryLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    FriendId = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    FriendCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendCategoryLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendCategoryLinks_FriendCategories_FriendCategoryId",
                        column: x => x.FriendCategoryId,
                        principalTable: "FriendCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FriendCategories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("5b357d96-741f-4957-bd3a-fb70adf0a5fa"), "Родственники" },
                    { new Guid("64ac57a0-1fcc-4e20-9ee4-2035bc6d787c"), "Default" },
                    { new Guid("b625b3a1-589e-4cff-916f-4a4dde52809c"), "Школьные друзя" },
                    { new Guid("eca5a630-fb54-4c1c-9b88-c4979ffa04c7"), "Близкие друзя" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendCategoryLinks_FriendCategoryId",
                table: "FriendCategoryLinks",
                column: "FriendCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendCategoryLinks");

            migrationBuilder.DropTable(
                name: "FriendCategories");
        }
    }
}
