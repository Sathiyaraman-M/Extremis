using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Extremis.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDurationAndExpireTimeInProposal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "UserTokens",
                schema: "dbo",
                newName: "UserTokens");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "dbo",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                schema: "dbo",
                newName: "UserLogins");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                schema: "dbo",
                newName: "UserClaims");

            migrationBuilder.RenameTable(
                name: "RoleClaims",
                schema: "dbo",
                newName: "RoleClaims");

            migrationBuilder.RenameColumn(
                name: "TimeOutTime",
                table: "Proposals",
                newName: "ExpireTime");

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "Proposals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "UserTokens",
                newName: "UserTokens",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRoles",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                newName: "UserLogins",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                newName: "UserClaims",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "RoleClaims",
                newName: "RoleClaims",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "ExpireTime",
                table: "Proposals",
                newName: "TimeOutTime");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Duration",
                table: "Proposals",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
