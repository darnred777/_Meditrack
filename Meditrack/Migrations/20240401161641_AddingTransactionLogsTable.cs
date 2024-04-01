using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meditrack.Migrations
{
    /// <inheritdoc />
    public partial class AddingTransactionLogsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionLogs",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    POHdrID = table.Column<int>(type: "int", nullable: true),
                    PRHdrID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    TransDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionLogs", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_TransactionLogs_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransactionLogs_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_TransactionLogs_PurchaseOrderHeader_POHdrID",
                        column: x => x.POHdrID,
                        principalTable: "PurchaseOrderHeader",
                        principalColumn: "POHdrID");
                    table.ForeignKey(
                        name: "FK_TransactionLogs_PurchaseRequisitionHeader_PRHdrID",
                        column: x => x.PRHdrID,
                        principalTable: "PurchaseRequisitionHeader",
                        principalColumn: "PRHdrID");
                    table.ForeignKey(
                        name: "FK_TransactionLogs_Status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLogs_Id",
                table: "TransactionLogs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLogs_POHdrID",
                table: "TransactionLogs",
                column: "POHdrID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLogs_PRHdrID",
                table: "TransactionLogs",
                column: "PRHdrID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLogs_ProductID",
                table: "TransactionLogs",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLogs_StatusID",
                table: "TransactionLogs",
                column: "StatusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionLogs");
        }
    }
}
