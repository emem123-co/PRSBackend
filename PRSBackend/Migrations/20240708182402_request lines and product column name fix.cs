using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRSBackend.Migrations
{
    /// <inheritdoc />
    public partial class requestlinesandproductcolumnnamefix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PartNumber",
                table: "Products",
                newName: "PartNbr");

            migrationBuilder.RenameIndex(
                name: "IX_Products_PartNumber",
                table: "Products",
                newName: "IX_Products_PartNbr");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PartNbr",
                table: "Products",
                newName: "PartNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Products_PartNbr",
                table: "Products",
                newName: "IX_Products_PartNumber");
        }
    }
}
