using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meditrack.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationType = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    LocationAddress = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    CategoryDescription = table.Column<string>(type: "text", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime", nullable: false),
                    TotalQuantityInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    UserGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGroupName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.UserGroupID);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    SupplierName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    ContactPerson = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    ContactNumber = table.Column<string>(type: "char(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    OfficeAddress = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierID);
                    table.ForeignKey(
                        name: "FK_Supplier_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "LocationID");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginTime_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    SKU = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Brand = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    ProductDescription = table.Column<string>(type: "varchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    UnitOfMeasurement = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUnitPriceUpdated = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastQuantityInStockUpdated = table.Column<DateTime>(type: "datetime", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "ProductCategory",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRequisitionHeader",
                columns: table => new
                {
                    PRHdrID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "MONEY", nullable: false),
                    PRDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequisitionHeader", x => x.PRHdrID);
                    table.ForeignKey(
                        name: "FK_PurchaseRequisitionHeader_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseRequisitionHeader_Status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseRequisitionHeader_Supplier_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Supplier",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroupMatrix",
                columns: table => new
                {
                    UserGroupMatrixID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    UserGroupID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupMatrix", x => x.UserGroupMatrixID);
                    table.ForeignKey(
                        name: "FK_UserGroupMatrix_UserGroup_UserGroupID",
                        column: x => x.UserGroupID,
                        principalTable: "UserGroup",
                        principalColumn: "UserGroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroupMatrix_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    TotalAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PODate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
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
                        onDelete: ReferentialAction.NoAction);
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

            migrationBuilder.CreateTable(
                name: "PurchaseRequisitionDetail",
                columns: table => new
                {
                    PRDtlID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRHdrID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    UnitOfMeasurement = table.Column<string>(type: "char(10)", maxLength: 10, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "PurchaseOrderDetail",
                columns: table => new
                {
                    PODtlID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    POHdrID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    UnitOfMeasurement = table.Column<string>(type: "char(10)", maxLength: 10, nullable: false),
                    QuantityInOrder = table.Column<int>(type: "int", nullable: false),
                    IsVATExclusive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderDetail", x => x.PODtlID);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetail_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetail_PurchaseOrderHeader_POHdrID",
                        column: x => x.POHdrID,
                        principalTable: "PurchaseOrderHeader",
                        principalColumn: "POHdrID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionLogs",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    POHdrID = table.Column<int>(type: "int", nullable: true),
                    PRHdrID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    TransDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionLogs", x => x.TransactionID);
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
                    table.ForeignKey(
                        name: "FK_TransactionLogs_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryID",
                table: "Product",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetail_POHdrID",
                table: "PurchaseOrderDetail",
                column: "POHdrID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetail_ProductID",
                table: "PurchaseOrderDetail",
                column: "ProductID");

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

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequisitionDetail_PRHdrID",
                table: "PurchaseRequisitionDetail",
                column: "PRHdrID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequisitionDetail_ProductID",
                table: "PurchaseRequisitionDetail",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequisitionHeader_LocationID",
                table: "PurchaseRequisitionHeader",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequisitionHeader_StatusID",
                table: "PurchaseRequisitionHeader",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequisitionHeader_SupplierID",
                table: "PurchaseRequisitionHeader",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_LocationID",
                table: "Supplier",
                column: "LocationID");

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

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLogs_UserID",
                table: "TransactionLogs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_User_LocationID",
                table: "User",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupMatrix_UserGroupID",
                table: "UserGroupMatrix",
                column: "UserGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupMatrix_UserID",
                table: "UserGroupMatrix",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseOrderDetail");

            migrationBuilder.DropTable(
                name: "PurchaseRequisitionDetail");

            migrationBuilder.DropTable(
                name: "TransactionLogs");

            migrationBuilder.DropTable(
                name: "UserGroupMatrix");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "PurchaseOrderHeader");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "PurchaseRequisitionHeader");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
