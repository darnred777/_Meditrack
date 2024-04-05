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

//        public void PopulatePurchaseDetailFromRequisition(int prhDtlId)
//        {
//            // Retrieve the PurchaseRequisitionDetail based on the provided PRDtlID
//            var purchaseRequisitionDetail = _context.PurchaseRequisitionDetail
//                                                    .Include(pr => pr.Product)
//                                                    .Include(pr => pr.PRHdrID)
//                                                    .Include(pr => pr.UnitPrice)
//                                                    .Include(pr => pr.UnitOfMeasurement)
//                                                    .Include(pr => pr.QuantityInOrder)
//                                                    .Include(pr => pr.Subtotal)
//                                                    .FirstOrDefault(pr => pr.PRDtlID == prhDtlId);        
//            if (purchaseRequisitionDetail != null)
//            {
//                // Create a new PurchaseOrderHeader instance
//                var purchaseOrderDetail = new PurchaseOrderDetail
//                {
//                    Product = purchaseRequisitionDetail.Product, 
//                    UnitPrice = purchaseRequisitionDetail.UnitPrice,
//                    UnitOfMeasurement = purchaseRequisitionDetail.UnitOfMeasurement,
//                    //PurchaseRequisitionDetail = purchaseRequisitionDetail,                  
//                    PODtlID = purchaseRequisitionDetail.PRDtlID,
//                    PurchaseOrderHeader = purchaseOrderHeader,
//                    QuantityInOrder = purchaseRequisitionDetail.QuantityInOrder,
//                    ProductID = (int)purchaseRequisitionDetail.ProductID,
//                    Subtotal = purchaseRequisitionDetail.Subtotal
//                };  

//                // Save the new PurchaseOrderHeader to the database
//                _context.PurchaseOrderDetail.Add(purchaseOrderDetail);
//                _context.SaveChanges();
//            }
//            else
//            {
//                // Handle the case where PurchaseRequisitionHeader with the given PRHdrID is not found
//                throw new Exception("PurchaseRequisitionDetail with the given ID was not found.");
//            }
//        }

//    }
//}
