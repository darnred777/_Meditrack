using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meditrack.Migrations
{
    /// <inheritdoc />
    public partial class ChangingTheProductIdToProductID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitoring_Product_ProductId",
                table: "Monitoring");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Monitoring",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_Monitoring_ProductId",
                table: "Monitoring",
                newName: "IX_Monitoring_ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitoring_Product_ProductID",
                table: "Monitoring",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitoring_Product_ProductID",
                table: "Monitoring");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Monitoring",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Monitoring_ProductID",
                table: "Monitoring",
                newName: "IX_Monitoring_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitoring_Product_ProductId",
                table: "Monitoring",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
