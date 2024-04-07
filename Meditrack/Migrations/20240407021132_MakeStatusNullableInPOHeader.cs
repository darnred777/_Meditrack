using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meditrack.Migrations
{
    /// <inheritdoc />
    public partial class MakeStatusNullableInPOHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderHeader_Status_StatusID",
                table: "PurchaseOrderHeader");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderHeader_Status_StatusID",
                table: "PurchaseOrderHeader");

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
        }
    }
}
