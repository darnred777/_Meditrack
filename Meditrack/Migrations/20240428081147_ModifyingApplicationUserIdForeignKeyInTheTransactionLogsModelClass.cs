using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meditrack.Migrations
{
    /// <inheritdoc />
    public partial class ModifyingApplicationUserIdForeignKeyInTheTransactionLogsModelClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLogs_AspNetUsers_Id",
                table: "TransactionLogs");

            migrationBuilder.DropIndex(
                name: "IX_TransactionLogs_Id",
                table: "TransactionLogs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TransactionLogs");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "TransactionLogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLogs_ApplicationUserId",
                table: "TransactionLogs",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLogs_AspNetUsers_ApplicationUserId",
                table: "TransactionLogs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLogs_AspNetUsers_ApplicationUserId",
                table: "TransactionLogs");

            migrationBuilder.DropIndex(
                name: "IX_TransactionLogs_ApplicationUserId",
                table: "TransactionLogs");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "TransactionLogs");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "TransactionLogs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLogs_Id",
                table: "TransactionLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLogs_AspNetUsers_Id",
                table: "TransactionLogs",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
