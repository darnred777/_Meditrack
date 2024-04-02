using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meditrack.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplicationUserClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Location_LocationID",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Location_LocationID",
                table: "AspNetUsers",
                column: "LocationID",
                principalTable: "Location",
                principalColumn: "LocationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Location_LocationID",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Location_LocationID",
                table: "AspNetUsers",
                column: "LocationID",
                principalTable: "Location",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
