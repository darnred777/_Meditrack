using Microsoft.EntityFrameworkCore;
using Meditrack.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Meditrack.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UserGroup> UserGroup { get; set; }

        public DbSet<Status> Status { get; set; }

        public DbSet<Location> Location { get; set; }

        public DbSet<ProductCategory> ProductCategory { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserGroupMatrix> UserGroupMatrix { get; set; }

        public DbSet<Supplier> Supplier { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<PurchaseRequisitionHeader> PurchaseRequisitionHeader { get; set; }

        public DbSet<PurchaseRequisitionDetail> PurchaseRequisitionDetail { get; set; }

        public DbSet<PurchaseOrderHeader> PurchaseOrderHeader { get; set; }

        public DbSet<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }

        public DbSet<TransactionLogs> TransactionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserGroup>().HasData(
            //    new UserGroup { UserGroupID = 1, UserGroupName = "Admin" },
            //    new UserGroup { UserGroupID = 1, UserGroupName = "Inventory Officer" },
            //    new UserGroup { UserGroupID = 1, UserGroupName = "Approver" },
            //    new UserGroup { UserGroupID = 1, UserGroupName = "Viewer" }
            //    );

            modelBuilder.Entity<Product>()
                .Property(p => p.UnitPrice)
                .HasColumnType("DECIMAL(10,2)");

            modelBuilder.Entity<PurchaseRequisitionDetail>()
                .Property(p => p.UnitPrice)
                .HasColumnType("DECIMAL(10,2)");

            modelBuilder.Entity<PurchaseRequisitionDetail>()
                .Property(p => p.Subtotal)
                .HasColumnType("MONEY");

            modelBuilder.Entity<PurchaseOrderDetail>()
                .Property(p => p.UnitPrice)
                .HasColumnType("DECIMAL(10,2)");

            modelBuilder.Entity<PurchaseOrderHeader>()
                .Property(p => p.TotalAmount)
                .HasColumnType("DECIMAL(10, 2)")
                .IsRequired();


            base.OnModelCreating(modelBuilder);
        }
    }
}
