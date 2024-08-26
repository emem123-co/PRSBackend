using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRSBackend.Migrations
{
    /// <inheritdoc />
    public partial class removerestrictonrequestlinefkonrequesttable : Migration
   {
    /// <inheritdoc />
    
    
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
                onDelete: ReferentialAction.SetNull);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)

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
                onDelete: ReferentialAction.NoAction);
        }

          
    }
}