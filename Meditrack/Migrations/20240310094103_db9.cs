using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meditrack.Migrations
{
    /// <inheritdoc />
    public partial class db9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseOrderHeader",
                columns: table => new
                {
                    POHdrID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    PRHdrID = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "MONEY", nullable: false),
                    PODate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderHeader", x => x.POHdrID);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderHeader_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderHeader_PurchaseRequisitionHeader_PRHdrID",
                        column: x => x.PRHdrID,
                        principalTable: "PurchaseRequisitionHeader",
                        principalColumn: "PRHdrID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderHeader_Status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderHeader_Supplier_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Supplier",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderHeader_LocationID",
                table: "PurchaseOrderHeader",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderHeader_PRHdrID",
                table: "PurchaseOrderHeader",
                column: "PRHdrID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderHeader_StatusID",
                table: "PurchaseOrderHeader",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderHeader_SupplierID",
                table: "PurchaseOrderHeader",
                column: "SupplierID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseOrderHeader");
        }
    }
}
