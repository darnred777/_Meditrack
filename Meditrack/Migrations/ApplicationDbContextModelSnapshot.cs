﻿// <auto-generated />
using System;
using Meditrack.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Meditrack.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.1.24081.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Meditrack.Models.Location", b =>
                {
                    b.Property<int>("LocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LocationID"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("LocationAddress")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LocationType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("LocationID");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("Meditrack.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastQuantityInStockUpdated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUnitPriceUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("UnitOfMeasurement")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("DECIMAL(10,2)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Meditrack.Models.ProductCategory", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CategoryDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("DateLastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalQuantityInStock")
                        .HasColumnType("int");

                    b.HasKey("CategoryID");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("Meditrack.Models.PurchaseOrderDetail", b =>
                {
                    b.Property<int>("PODtlID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PODtlID"));

                    b.Property<bool>("IsVATExclusive")
                        .HasColumnType("bit");

                    b.Property<int>("POHdrID")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("QuantityInOrder")
                        .HasColumnType("int");

                    b.Property<string>("UnitOfMeasurement")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("DECIMAL(10,2)");

                    b.HasKey("PODtlID");

                    b.HasIndex("POHdrID");

                    b.HasIndex("ProductID");

                    b.ToTable("PurchaseOrderDetail");
                });

            modelBuilder.Entity("Meditrack.Models.PurchaseOrderHeader", b =>
                {
                    b.Property<int>("POHdrID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("POHdrID"));

                    b.Property<int>("LocationID")
                        .HasColumnType("int");

                    b.Property<DateTime>("PODate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PRHdrID")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("StatusID")
                        .HasColumnType("int");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("DECIMAL(10, 2)");

                    b.HasKey("POHdrID");

                    b.HasIndex("LocationID");

                    b.HasIndex("PRHdrID");

                    b.HasIndex("StatusID");

                    b.HasIndex("SupplierID");

                    b.ToTable("PurchaseOrderHeader");
                });

            modelBuilder.Entity("Meditrack.Models.PurchaseRequisitionDetail", b =>
                {
                    b.Property<int>("PRDtlID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PRDtlID"));

                    b.Property<int>("PRHdrID")
                        .HasColumnType("int");

                    b.Property<int?>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("QuantityInOrder")
                        .HasColumnType("int");

                    b.Property<decimal>("Subtotal")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("MONEY");

                    b.Property<string>("UnitOfMeasurement")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("DECIMAL(10,2)");

                    b.HasKey("PRDtlID");

                    b.HasIndex("PRHdrID");

                    b.HasIndex("ProductID");

                    b.ToTable("PurchaseRequisitionDetail");
                });

            modelBuilder.Entity("Meditrack.Models.PurchaseRequisitionHeader", b =>
                {
                    b.Property<int>("PRHdrID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PRHdrID"));

                    b.Property<int>("LocationID")
                        .HasColumnType("int");

                    b.Property<DateTime>("PRDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusID")
                        .HasColumnType("int");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("MONEY");

                    b.HasKey("PRHdrID");

                    b.HasIndex("LocationID");

                    b.HasIndex("StatusID");

                    b.HasIndex("SupplierID");

                    b.ToTable("PurchaseRequisitionHeader");
                });

            modelBuilder.Entity("Meditrack.Models.Status", b =>
                {
                    b.Property<int>("StatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusID"));

                    b.Property<string>("StatusDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusID");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("Meditrack.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplierID"));

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("ContactPerson")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("LocationID")
                        .HasColumnType("int");

                    b.Property<string>("OfficeAddress")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("SupplierID");

                    b.HasIndex("LocationID");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("Meditrack.Models.TransactionLogs", b =>
                {
                    b.Property<int>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionID"));

                    b.Property<int?>("POHdrID")
                        .HasColumnType("int");

                    b.Property<int?>("PRHdrID")
                        .HasColumnType("int");

                    b.Property<int?>("ProductID")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("StatusID")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransType")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("TransactionID");

                    b.HasIndex("POHdrID");

                    b.HasIndex("PRHdrID");

                    b.HasIndex("ProductID");

                    b.HasIndex("StatusID");

                    b.HasIndex("UserID");

                    b.ToTable("TransactionLogs");
                });

            modelBuilder.Entity("Meditrack.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastLoginTime_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("LocationID")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("UserID");

                    b.HasIndex("LocationID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Meditrack.Models.UserGroup", b =>
                {
                    b.Property<int>("UserGroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserGroupID"));

                    b.Property<string>("UserGroupName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("UserGroupID");

                    b.ToTable("UserGroup");
                });

            modelBuilder.Entity("Meditrack.Models.UserGroupMatrix", b =>
                {
                    b.Property<int>("UserGroupMatrixID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserGroupMatrixID"));

                    b.Property<int>("UserGroupID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("UserGroupMatrixID");

                    b.HasIndex("UserGroupID");

                    b.HasIndex("UserID");

                    b.ToTable("UserGroupMatrix");
                });

            modelBuilder.Entity("Meditrack.Models.Product", b =>
                {
                    b.HasOne("Meditrack.Models.ProductCategory", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("Meditrack.Models.PurchaseOrderDetail", b =>
                {
                    b.HasOne("Meditrack.Models.PurchaseOrderHeader", "PurchaseOrderHeader")
                        .WithMany()
                        .HasForeignKey("POHdrID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Meditrack.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("PurchaseOrderHeader");
                });

            modelBuilder.Entity("Meditrack.Models.PurchaseOrderHeader", b =>
                {
                    b.HasOne("Meditrack.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Meditrack.Models.PurchaseRequisitionHeader", "PurchaseRequisitionHeader")
                        .WithMany()
                        .HasForeignKey("PRHdrID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Meditrack.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Meditrack.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("PurchaseRequisitionHeader");

                    b.Navigation("Status");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Meditrack.Models.PurchaseRequisitionDetail", b =>
                {
                    b.HasOne("Meditrack.Models.PurchaseRequisitionHeader", "PurchaseRequisitionHeader")
                        .WithMany()
                        .HasForeignKey("PRHdrID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Meditrack.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.Navigation("Product");

                    b.Navigation("PurchaseRequisitionHeader");
                });

            modelBuilder.Entity("Meditrack.Models.PurchaseRequisitionHeader", b =>
                {
                    b.HasOne("Meditrack.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Meditrack.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Meditrack.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Status");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Meditrack.Models.Supplier", b =>
                {
                    b.HasOne("Meditrack.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Meditrack.Models.TransactionLogs", b =>
                {
                    b.HasOne("Meditrack.Models.PurchaseOrderHeader", "PurchaseOrderHeader")
                        .WithMany()
                        .HasForeignKey("POHdrID");

                    b.HasOne("Meditrack.Models.PurchaseRequisitionHeader", "PurchaseRequisitionHeader")
                        .WithMany()
                        .HasForeignKey("PRHdrID");

                    b.HasOne("Meditrack.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.HasOne("Meditrack.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Meditrack.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("PurchaseOrderHeader");

                    b.Navigation("PurchaseRequisitionHeader");

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Meditrack.Models.User", b =>
                {
                    b.HasOne("Meditrack.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Meditrack.Models.UserGroupMatrix", b =>
                {
                    b.HasOne("Meditrack.Models.UserGroup", "UserGroup")
                        .WithMany()
                        .HasForeignKey("UserGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Meditrack.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("UserGroup");
                });
#pragma warning restore 612, 618
        }
    }
}
