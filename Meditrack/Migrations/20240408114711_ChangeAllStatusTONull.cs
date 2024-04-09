using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meditrack.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAllStatusTONull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderHeader_Status_StatusID",
                table: "PurchaseOrderHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequisitionHeader_Status_StatusID",
                table: "PurchaseRequisitionHeader");

            migrationBuilder.AlterColumn<int>(
                name: "StatusID",
                table: "PurchaseRequisitionHeader",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StatusID",
                table: "PurchaseOrderHeader",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderHeader_Status_StatusID",
                table: "PurchaseOrderHeader",
                column: "StatusID",
                principalTable: "Status",
                principalColumn: "StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequisitionHeader_Status_StatusID",
                table: "PurchaseRequisitionHeader",
                column: "StatusID",
                principalTable: "Status",
                principalColumn: "StatusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderHeader_Status_StatusID",
                table: "PurchaseOrderHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequisitionHeader_Status_StatusID",
                table: "PurchaseRequisitionHeader");

            migrationBuilder.AlterColumn<int>(
                name: "StatusID",
                table: "PurchaseRequisitionHeader",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusID",
                table: "PurchaseOrderHeader",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderHeader_Status_StatusID",
                table: "PurchaseOrderHeader",
                column: "StatusID",
                principalTable: "Status",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequisitionHeader_Status_StatusID",
                table: "PurchaseRequisitionHeader",
                column: "StatusID",
                principalTable: "Status",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
