using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership_Managment.Migrations
{
    /// <inheritdoc />
    public partial class tableLoggFieldAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActionDate",
                table: "Members",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActionType",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActionDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActionType",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionDate",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "ActionType",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "ActionDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ActionType",
                table: "Documents");
        }
    }
}
