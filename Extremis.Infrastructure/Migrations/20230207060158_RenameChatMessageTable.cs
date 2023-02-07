using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Extremis.Migrations
{
    /// <inheritdoc />
    public partial class RenameChatMessageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessage_Projects_ProjectId",
                table: "ChatMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessage_Users_FromUserId",
                table: "ChatMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessage_Users_ToUserId",
                table: "ChatMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatMessage",
                table: "ChatMessage");

            migrationBuilder.RenameTable(
                name: "ChatMessage",
                newName: "ChatMesages");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessage_ToUserId",
                table: "ChatMesages",
                newName: "IX_ChatMesages_ToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessage_ProjectId",
                table: "ChatMesages",
                newName: "IX_ChatMesages_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessage_FromUserId",
                table: "ChatMesages",
                newName: "IX_ChatMesages_FromUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatMesages",
                table: "ChatMesages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMesages_Projects_ProjectId",
                table: "ChatMesages",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMesages_Users_FromUserId",
                table: "ChatMesages",
                column: "FromUserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMesages_Users_ToUserId",
                table: "ChatMesages",
                column: "ToUserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMesages_Projects_ProjectId",
                table: "ChatMesages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMesages_Users_FromUserId",
                table: "ChatMesages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMesages_Users_ToUserId",
                table: "ChatMesages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatMesages",
                table: "ChatMesages");

            migrationBuilder.RenameTable(
                name: "ChatMesages",
                newName: "ChatMessage");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMesages_ToUserId",
                table: "ChatMessage",
                newName: "IX_ChatMessage_ToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMesages_ProjectId",
                table: "ChatMessage",
                newName: "IX_ChatMessage_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMesages_FromUserId",
                table: "ChatMessage",
                newName: "IX_ChatMessage_FromUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatMessage",
                table: "ChatMessage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessage_Projects_ProjectId",
                table: "ChatMessage",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessage_Users_FromUserId",
                table: "ChatMessage",
                column: "FromUserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessage_Users_ToUserId",
                table: "ChatMessage",
                column: "ToUserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
