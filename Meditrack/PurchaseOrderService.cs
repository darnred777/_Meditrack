using Meditrack.Data;
using Meditrack.Models;
using Microsoft.EntityFrameworkCore;

namespace Meditrack.Services
{
    public class PurchaseOrderService
    {
        private readonly ApplicationDbContext _context;

        public PurchaseOrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void PopulatePurchaseOrderFromRequisition(int prHdrId)
        {
            // Retrieve the PurchaseRequisitionHeader based on the provided PRHdrID
            var purchaseRequisitionHeader = _context.PurchaseRequisitionHeader
                                                    .Include(pr => pr.Location)
                                                    .Include(pr => pr.Status)
                                                    .Include(pr => pr.Supplier)
                                                    .Include(pr => pr.PRDate)
                                                    .FirstOrDefault(pr => pr.PRHdrID == prHdrId);        
            if (purchaseRequisitionHeader != null)
            {
                // Create a new PurchaseOrderHeader instance
                var purchaseOrderHeader = new PurchaseOrderHeader
                {                  
                    Status = purchaseRequisitionHeader.Status, // Provide the required Status object here
                    Supplier = purchaseRequisitionHeader.Supplier, // Use the Supplier from PurchaseRequisitionHeader
                    Location = purchaseRequisitionHeader.Location, // Use the Location from PurchaseRequisitionHeader
                    PurchaseRequisitionHeader = purchaseRequisitionHeader,
                    PODate = purchaseRequisitionHeader.PRDate,
                    PRHdrID = purchaseRequisitionHeader.PRHdrID,
                    TotalAmount = purchaseRequisitionHeader.TotalAmount,
                    LocationID = purchaseRequisitionHeader.LocationID,
                    StatusID = (int)purchaseRequisitionHeader.StatusID,
                    SupplierID = purchaseRequisitionHeader.SupplierID
                };

                // Save the new PurchaseOrderHeader to the database
                _context.PurchaseOrderHeader.Add(purchaseOrderHeader);
                _context.SaveChanges();
            }
            else
            {
                // Handle the case where PurchaseRequisitionHeader with the given PRHdrID is not found
                throw new Exception("PurchaseRequisitionHeader with the given ID was not found.");
            }
        }
    }
}
