using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Meditrack.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            // Fetch the existing product for comparison (without tracking)
            var existingProduct = _db.Product
                                     .AsNoTracking()
                                     .FirstOrDefault(p => p.ProductID == obj.ProductID);

            if (existingProduct == null)
            {
                throw new InvalidOperationException("Product not found.");
            }

            // Determine if critical properties have changed
            bool unitPriceChanged = existingProduct.UnitPrice != obj.UnitPrice;
            bool unitOfMeasurementChanged = existingProduct.UnitOfMeasurement != obj.UnitOfMeasurement;

            // Apply the update to the product
            _db.Product.Update(obj);

            // If important properties have changed, update related details
            if (unitPriceChanged || unitOfMeasurementChanged)
            {
                // Update Purchase Requisition Details
                var requisitionDetails = _db.PurchaseRequisitionDetail.Where(p => p.ProductID == obj.ProductID);
                foreach (var detail in requisitionDetails)
                {
                    if (unitPriceChanged) detail.UnitPrice = obj.UnitPrice;
                    if (unitOfMeasurementChanged) detail.UnitOfMeasurement = obj.UnitOfMeasurement;
                }

                // Update Purchase Order Details
                var orderDetails = _db.PurchaseOrderDetail.Where(p => p.ProductID == obj.ProductID);
                foreach (var detail in orderDetails)
                {
                    if (unitPriceChanged) detail.UnitPrice = obj.UnitPrice;
                    if (unitOfMeasurementChanged) detail.UnitOfMeasurement = obj.UnitOfMeasurement;
                }
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _db.Product.ToList();
        }

        // Additional repository methods as needed
        public IQueryable<Product> Products => _db.Product;
        public Product GetProductById(int id)
        {
            return _db.Product.FirstOrDefault(p => p.ProductID == id);
        }

        public bool HasDependencies(int productId)
        {
            return _db.PurchaseRequisitionDetail.Any(p => p.ProductID == productId) ||
                   _db.PurchaseOrderDetail.Any(p => p.ProductID == productId) ||
                   _db.TransactionLogs.Any(t => t.ProductID == productId);
        }

        public IEnumerable<Product> GetExpiringProducts(DateOnly startDate, DateOnly endDate)
        {
            return _db.Product.Where(p => p.ExpirationDate >= startDate && p.ExpirationDate <= endDate).ToList();
        }

        public IQueryable<Product> AsQueryable()
        {
            return _db.Product;
        }
    }
}
