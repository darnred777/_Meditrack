using Microsoft.EntityFrameworkCore;
using Meditrack.Models;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure UnitPrice for the Product entity
            modelBuilder.Entity<Product>()
                .Property(p => p.UnitPrice)
                .HasColumnType("DECIMAL(10,2)");

            // Configure UnitPrice and Subtotal for PurchaseRequisitionDetail entity
            modelBuilder.Entity<PurchaseRequisitionDetail>()
                .Property(p => p.UnitPrice)
                .HasColumnType("DECIMAL(10,2)");

            modelBuilder.Entity<PurchaseRequisitionDetail>()
                .Property(p => p.Subtotal)
                .HasColumnType("MONEY");

            // Configure TotalAmount for PurchaseOrderHeader entity
            modelBuilder.Entity<PurchaseOrderHeader>()
                .Property(p => p.TotalAmount)
                .HasColumnType("MONEY");

            base.OnModelCreating(modelBuilder);
        }
    }
}
