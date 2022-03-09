using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialRofl.Data.Migrations
{
    public partial class AddMainPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MainPhotoId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MainPhotoId",
                table: "Users",
                column: "MainPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_Hash",
                table: "Photos",
                column: "Hash",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Photos_MainPhotoId",
                table: "Users",
                column: "MainPhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Photos_MainPhotoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_MainPhotoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Photos_Hash",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "MainPhotoId",
                table: "Users");
        }
    }
}
