using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership_Managment.Migrations
{
    /// <inheritdoc />
    public partial class activationDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActivaitonDate",
                table: "Members",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivaitonDate",
                table: "Members");
        }
    }
}
