using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFriendCAtegoryLinkConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendCategoryLinks_Users_FriendId",
                table: "FriendCategoryLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendCategoryLinks_Users_UserId",
                table: "FriendCategoryLinks");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "FriendCategoryLinks",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FriendCategoryLinks_UserId1",
                table: "FriendCategoryLinks",
                column: "UserId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendCategoryLinks_Users_FriendId",
                table: "FriendCategoryLinks",
                column: "FriendId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendCategoryLinks_Users_UserId",
                table: "FriendCategoryLinks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendCategoryLinks_Users_UserId1",
                table: "FriendCategoryLinks",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendCategoryLinks_Users_FriendId",
                table: "FriendCategoryLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendCategoryLinks_Users_UserId",
                table: "FriendCategoryLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendCategoryLinks_Users_UserId1",
                table: "FriendCategoryLinks");

            migrationBuilder.DropIndex(
                name: "IX_FriendCategoryLinks_UserId1",
                table: "FriendCategoryLinks");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "FriendCategoryLinks");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendCategoryLinks_Users_FriendId",
                table: "FriendCategoryLinks",
                column: "FriendId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendCategoryLinks_Users_UserId",
                table: "FriendCategoryLinks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
