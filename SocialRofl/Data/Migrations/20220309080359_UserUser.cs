using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialRofl.Data.Migrations
{
    public partial class UserUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserUser");

            migrationBuilder.CreateTable(
                name: "Followers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SubscribedToId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followers", x => new { x.SubscribedToId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Followers_Users_SubscribedToId",
                        column: x => x.SubscribedToId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Followers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Followers_UserId",
                table: "Followers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Followers");

            migrationBuilder.CreateTable(
                name: "UserUser",
                columns: table => new
                {
                    FriendsId = table.Column<int>(type: "int", nullable: false),
                    OtherFriendsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUser", x => new { x.FriendsId, x.OtherFriendsId });
                    table.ForeignKey(
                        name: "FK_UserUser_Users_FriendsId",
                        column: x => x.FriendsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUser_Users_OtherFriendsId",
                        column: x => x.OtherFriendsId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserUser_OtherFriendsId",
                table: "UserUser",
                column: "OtherFriendsId");
        }
    }
}
