using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRSBackend.Migrations
{
    /// <inheritdoc />
    public partial class addcascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLines_Requests_RequestID",
                table: "RequestLines");

            migrationBuilder.AlterColumn<int>(
                name: "RequestID",
                table: "RequestLines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLines_Requests_RequestID",
                table: "RequestLines",
                column: "RequestID",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLines_Requests_RequestID",
                table: "RequestLines");

            migrationBuilder.AlterColumn<int>(
                name: "RequestID",
                table: "RequestLines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLines_Requests_RequestID",
                table: "RequestLines",
                column: "RequestID",
                principalTable: "Requests",
                principalColumn: "Id");
        }
    }
}
