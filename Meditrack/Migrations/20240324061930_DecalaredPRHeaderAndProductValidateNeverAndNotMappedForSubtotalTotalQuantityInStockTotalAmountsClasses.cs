using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meditrack.Migrations
{
    /// <inheritdoc />
    public partial class DecalaredPRHeaderAndProductValidateNeverAndNotMappedForSubtotalTotalQuantityInStockTotalAmountsClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "PurchaseRequisitionHeader");

            migrationBuilder.DropColumn(
                name: "TotalQuantityInStock",
                table: "ProductCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "PurchaseRequisitionHeader",
                type: "MONEY",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TotalQuantityInStock",
                table: "ProductCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
