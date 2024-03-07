using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership_Managment.Migrations
{
    /// <inheritdoc />
    public partial class feeCollectionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Members",
                newName: "PresentAddress");

            migrationBuilder.AddColumn<DateTime>(
                name: "Dob",
                table: "Members",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentAddress",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FeeCollections",
                columns: table => new
                {
                    CollectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CollectionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeCollections", x => x.CollectionId);
                    table.ForeignKey(
                        name: "FK_FeeCollections_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeeCollections_MemberId",
                table: "FeeCollections",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeeCollections");

            migrationBuilder.DropColumn(
                name: "Dob",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "PermanentAddress",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "PresentAddress",
                table: "Members",
                newName: "Address");
        }
    }
}
