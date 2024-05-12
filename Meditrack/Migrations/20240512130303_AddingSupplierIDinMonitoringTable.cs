using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meditrack.Migrations
{
    /// <inheritdoc />
    public partial class AddingSupplierIDinMonitoringTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierID",
                table: "Monitoring",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Monitoring_SupplierID",
                table: "Monitoring",
                column: "SupplierID");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitoring_Supplier_SupplierID",
                table: "Monitoring",
                column: "SupplierID",
                principalTable: "Supplier",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitoring_Supplier_SupplierID",
                table: "Monitoring");

            migrationBuilder.DropIndex(
                name: "IX_Monitoring_SupplierID",
                table: "Monitoring");

            migrationBuilder.DropColumn(
                name: "SupplierID",
                table: "Monitoring");
        }
    }
}
