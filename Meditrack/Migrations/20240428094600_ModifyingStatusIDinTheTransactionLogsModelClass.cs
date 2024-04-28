using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meditrack.Migrations
{
    /// <inheritdoc />
    public partial class ModifyingStatusIDinTheTransactionLogsModelClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLogs_Status_StatusID",
                table: "TransactionLogs");

            migrationBuilder.AlterColumn<int>(
                name: "StatusID",
                table: "TransactionLogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLogs_Status_StatusID",
                table: "TransactionLogs",
                column: "StatusID",
                principalTable: "Status",
                principalColumn: "StatusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLogs_Status_StatusID",
                table: "TransactionLogs");

            migrationBuilder.AlterColumn<int>(
                name: "StatusID",
                table: "TransactionLogs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLogs_Status_StatusID",
                table: "TransactionLogs",
                column: "StatusID",
                principalTable: "Status",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
