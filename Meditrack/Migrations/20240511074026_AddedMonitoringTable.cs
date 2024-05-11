using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meditrack.Migrations
{
    /// <inheritdoc />
    public partial class AddedMonitoringTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Monitoring",
                columns: table => new
                {
                    MonitoringID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    QuantityReceived = table.Column<int>(type: "int", nullable: false),
                    QuantityExpected = table.Column<int>(type: "int", nullable: false),
                    QuantityLacking = table.Column<int>(type: "int", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitoring", x => x.MonitoringID);
                    table.ForeignKey(
                        name: "FK_Monitoring_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Monitoring_ProductId",
                table: "Monitoring",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Monitoring");
        }
    }
}
