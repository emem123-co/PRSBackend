using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRSBackend.Migrations
{
    /// <inheritdoc />
    public partial class thereisnewfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLines_Requests_RequestID",
                table: "RequestLines");

            

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

         
        
        

        }
    }
}
