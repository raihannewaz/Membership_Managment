using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership_Managment.Migrations
{
    /// <inheritdoc />
    public partial class DocumentModelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UtilityBillPhoto",
                table: "Documents",
                newName: "DocumentType");

            migrationBuilder.RenameColumn(
                name: "NidPhoto",
                table: "Documents",
                newName: "DocumentLocation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentType",
                table: "Documents",
                newName: "UtilityBillPhoto");

            migrationBuilder.RenameColumn(
                name: "DocumentLocation",
                table: "Documents",
                newName: "NidPhoto");
        }
    }
}
