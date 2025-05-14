using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mo3tarb.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddChatMessagesWithUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "ChatMessages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_AppUserId",
                table: "ChatMessages",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_AspNetUsers_AppUserId",
                table: "ChatMessages",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_AspNetUsers_AppUserId",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_AppUserId",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ChatMessages");
        }
    }
}
