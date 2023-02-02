using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Extremis.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStatusAndExpireTimeFromProposalEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireTime",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Proposals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireTime",
                table: "Proposals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Proposals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
