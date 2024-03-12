using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meditrack.Migrations
{
    /// <inheritdoc />
    public partial class db8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseRequisitionDetail",
                columns: table => new
                {
                    PRDtlID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRHdrID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    UnitOfMeasurement = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    QuantityInOrder = table.Column<int>(type: "int", nullable: false),
                    Subtotal = table.Column<decimal>(type: "MONEY", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequisitionDetail", x => x.PRDtlID);
                    table.ForeignKey(
                        name: "FK_PurchaseRequisitionDetail_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_PurchaseRequisitionDetail_PurchaseRequisitionHeader_PRHdrID",
                        column: x => x.PRHdrID,
                        principalTable: "PurchaseRequisitionHeader",
                        principalColumn: "PRHdrID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequisitionDetail_PRHdrID",
                table: "PurchaseRequisitionDetail",
                column: "PRHdrID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequisitionDetail_ProductID",
                table: "PurchaseRequisitionDetail",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseRequisitionDetail");
        }
    }
}
