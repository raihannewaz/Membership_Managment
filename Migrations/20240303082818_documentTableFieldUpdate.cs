using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership_Managment.Migrations
{
    /// <inheritdoc />
    public partial class documentTableFieldUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentPhoto",
                table: "Documents",
                newName: "UtilityBillPhoto");

            migrationBuilder.AddColumn<string>(
                name: "NidPhoto",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NidPhoto",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "UtilityBillPhoto",
                table: "Documents",
                newName: "DocumentPhoto");
        }
    }
}
