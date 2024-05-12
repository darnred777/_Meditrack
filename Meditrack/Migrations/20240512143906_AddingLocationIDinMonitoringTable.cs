using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meditrack.Migrations
{
    /// <inheritdoc />
    public partial class AddingLocationIDinMonitoringTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationID",
                table: "Monitoring",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Monitoring_LocationID",
                table: "Monitoring",
                column: "LocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitoring_Location_LocationID",
                table: "Monitoring",
                column: "LocationID",
                principalTable: "Location",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitoring_Location_LocationID",
                table: "Monitoring");

            migrationBuilder.DropIndex(
                name: "IX_Monitoring_LocationID",
                table: "Monitoring");

            migrationBuilder.DropColumn(
                name: "LocationID",
                table: "Monitoring");
        }
    }
}
