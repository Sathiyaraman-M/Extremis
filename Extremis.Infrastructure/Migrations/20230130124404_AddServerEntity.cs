using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Extremis.Migrations
{
    /// <inheritdoc />
    public partial class AddServerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Servers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servers_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerMember",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ServerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerMember_Servers_ServerId",
                        column: x => x.ServerId,
                        principalSchema: "dbo",
                        principalTable: "Servers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerMember_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServerMember_ServerId",
                table: "ServerMember",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerMember_UserId",
                table: "ServerMember",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_OwnerId",
                schema: "dbo",
                table: "Servers",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServerMember");

            migrationBuilder.DropTable(
                name: "Servers",
                schema: "dbo");
        }
    }
}
