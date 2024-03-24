using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meditrack.Migrations
{
    /// <inheritdoc />
    public partial class DeletingModelBuilderOfPRTotalAndSubTotals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "PurchaseRequisitionHeader",
                type: "DECIMAL(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Subtotal",
                table: "PurchaseRequisitionDetail",
                type: "DECIMAL(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "MONEY");

            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "PurchaseOrderDetail",
                type: "DECIMAL(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "PurchaseRequisitionHeader");

            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "PurchaseOrderDetail");

            migrationBuilder.AlterColumn<decimal>(
                name: "Subtotal",
                table: "PurchaseRequisitionDetail",
                type: "MONEY",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,2)");
        }
    }
}
