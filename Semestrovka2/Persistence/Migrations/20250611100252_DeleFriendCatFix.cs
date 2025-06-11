using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleFriendCatFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendCategoryLinks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FriendCategoryLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FriendCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    FriendId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FriendCategoryLinks_Users_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FriendCategoryLinks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendCategoryLinks_FriendCategoryId",
                table: "FriendCategoryLinks",
                column: "FriendCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendCategoryLinks_FriendId",
                table: "FriendCategoryLinks",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendCategoryLinks_UserId",
                table: "FriendCategoryLinks",
                column: "UserId");
        }
    }
}
