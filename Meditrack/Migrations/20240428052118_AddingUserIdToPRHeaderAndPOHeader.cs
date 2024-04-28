using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meditrack.Migrations
{
    /// <inheritdoc />
    public partial class AddingUserIdToPRHeaderAndPOHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequisitionDetail_Product_ProductID",
                table: "PurchaseRequisitionDetail");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "PurchaseRequisitionHeader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "PurchaseRequisitionHeader",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "PurchaseRequisitionDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "PurchaseOrderHeader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "PurchaseOrderHeader",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequisitionHeader_Id",
                table: "PurchaseRequisitionHeader",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderHeader_Id",
                table: "PurchaseOrderHeader",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderHeader_AspNetUsers_Id",
                table: "PurchaseOrderHeader",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequisitionDetail_Product_ProductID",
                table: "PurchaseRequisitionDetail",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequisitionHeader_AspNetUsers_Id",
                table: "PurchaseRequisitionHeader",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderHeader_AspNetUsers_Id",
                table: "PurchaseOrderHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequisitionDetail_Product_ProductID",
                table: "PurchaseRequisitionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequisitionHeader_AspNetUsers_Id",
                table: "PurchaseRequisitionHeader");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRequisitionHeader_Id",
                table: "PurchaseRequisitionHeader");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderHeader_Id",
                table: "PurchaseOrderHeader");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "PurchaseRequisitionHeader");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PurchaseRequisitionHeader");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "PurchaseOrderHeader");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PurchaseOrderHeader");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "PurchaseRequisitionDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequisitionDetail_Product_ProductID",
                table: "PurchaseRequisitionDetail",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID");
        }
    }
}
