//using Meditrack.Data;
//using Meditrack.Models;
//using Microsoft.EntityFrameworkCore;

//namespace Meditrack.Services
//{
//    public class PurchaseDetailService
//    {
//        private readonly ApplicationDbContext _context;

//        public PurchaseDetailService(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public void PopulatePurchaseOrderDetailFromRequisitionDetail(int prDtlId)
//        {
//            // Retrieve the PurchaseRequisitionDetail based on the provided PRDtlID
//            var purchaseRequisitionDetail = _context.PurchaseRequisitionDetail
//                                                    .Include(prd => prd.Product)
//                                                    .Include(prd => prd.PurchaseRequisitionHeader) // Include the PurchaseRequisitionHeader navigation property
//                                                    .FirstOrDefault(prd => prd.PRDtlID == prDtlId);

//            if (purchaseRequisitionDetail != null)
//            {
//                // Retrieve the associated PurchaseOrderHeader through a join
//                var purchaseOrderHeader = _context.PurchaseOrderHeader
//                                                .FirstOrDefault(poh => poh.POHdrID == purchaseRequisitionDetail.PurchaseRequisitionHeader.PRHdrID);

//                // Create a new PurchaseOrderDetail instance
//                var purchaseOrderDetail = new PurchaseOrderDetail
//                {
//                    PurchaseOrderHeader = purchaseOrderHeader, // Assign the retrieved PurchaseOrderHeader
//                    Product = purchaseRequisitionDetail.Product, // Use the Product from PurchaseRequisitionDetail
//                    UnitPrice = purchaseRequisitionDetail.UnitPrice,
//                    UnitOfMeasurement = purchaseRequisitionDetail.UnitOfMeasurement,
//                    QuantityInOrder = purchaseRequisitionDetail.QuantityInOrder,
//                    Subtotal = purchaseRequisitionDetail.Subtotal,
//                };

//                // Save the new PurchaseOrderDetail to the database
//                _context.PurchaseOrderDetail.Add(purchaseOrderDetail);
//                _context.SaveChanges();
//            }
//            else
//            {
//                // Handle the case where PurchaseRequisitionDetail with the given PRDtlID is not found
//                throw new Exception("PurchaseRequisitionDetail with the given ID was not found.");
//            }
//        }




//    }
//}
