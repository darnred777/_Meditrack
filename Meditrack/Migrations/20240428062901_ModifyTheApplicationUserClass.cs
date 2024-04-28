using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meditrack.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTheApplicationUserClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderHeader_AspNetUsers_Id",
                table: "PurchaseOrderHeader");

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
                name: "Id",
                table: "PurchaseRequisitionHeader");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PurchaseOrderHeader");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "PurchaseRequisitionHeader",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "PurchaseOrderHeader",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequisitionHeader_ApplicationUserId",
                table: "PurchaseRequisitionHeader",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderHeader_ApplicationUserId",
                table: "PurchaseOrderHeader",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderHeader_AspNetUsers_ApplicationUserId",
                table: "PurchaseOrderHeader",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequisitionHeader_AspNetUsers_ApplicationUserId",
                table: "PurchaseRequisitionHeader",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderHeader_AspNetUsers_ApplicationUserId",
                table: "PurchaseOrderHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequisitionHeader_AspNetUsers_ApplicationUserId",
                table: "PurchaseRequisitionHeader");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRequisitionHeader_ApplicationUserId",
                table: "PurchaseRequisitionHeader");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderHeader_ApplicationUserId",
                table: "PurchaseOrderHeader");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "PurchaseRequisitionHeader",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "PurchaseRequisitionHeader",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "PurchaseOrderHeader",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
                name: "FK_PurchaseRequisitionHeader_AspNetUsers_Id",
                table: "PurchaseRequisitionHeader",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
