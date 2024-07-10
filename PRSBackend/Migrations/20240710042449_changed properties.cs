using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRSBackend.Migrations
{
    /// <inheritdoc />
    public partial class changedproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Vendors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_ProductId",
                table: "Vendors",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Products_ProductId",
                table: "Vendors",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Products_ProductId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_ProductId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Vendors");
        }
    }
}
