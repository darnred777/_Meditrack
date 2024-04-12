using Meditrack.Models;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;

public class PurchaseOrderService : IPurchaseOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public PurchaseOrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void CreatePurchaseOrderFromRequisition(int prdId)
    {
        // Fetch the Purchase Requisition Header and its Details
        var prHeader = _unitOfWork.PurchaseRequisitionHeader
                        .GetFirstOrDefault(prh => prh.PRHdrID == prdId, includeProperties: "PurchaseRequisitionDetail");

        if (prHeader == null) throw new Exception("Purchase Requisition not found.");

        var statusApproved = _unitOfWork.Status.GetFirstOrDefault(s => s.StatusDescription == StaticDetails.Status_Approved);
        if (statusApproved == null) throw new Exception("Approved status not found in the database.");

        // Update the Status of the Purchase Requisition to 'Approved'
        prHeader.StatusID = statusApproved.StatusID;
        _unitOfWork.Save(); // Save the status update

        // Create a new Purchase Order Header
        var poHeader = new PurchaseOrderHeader()
        {
            // Assuming you have a method to map or copy properties
            // This is a simplified example; adapt property names as necessary
            Supplier = prHeader.Supplier,
            SupplierID = prHeader.SupplierID,
            Location = prHeader.Location,
            LocationID = prHeader.LocationID,
            Status = statusApproved, // Use the fetched status
            StatusID = statusApproved.StatusID,
            TotalAmount = (decimal)prHeader.TotalAmount,
            PurchaseRequisitionHeader = prHeader

            // Other necessary properties...
        };

        _unitOfWork.PurchaseOrderHeader.Add(poHeader);
        /*_unitOfWork.Save();*/ // Consider saving here to ensure POHdrID is populated

        // For each detail in the Purchase Requisition, create a Purchase Order Detail
        foreach (var prDetail in prHeader.PurchaseRequisitionDetail)
        {
            var poDetail = new PurchaseOrderDetail()
            {
                POHdrID = poHeader.POHdrID, // Ensure this ID is correctly assigned
                ProductID = (int)prDetail.ProductID,
                UnitPrice = prDetail.UnitPrice,
                Product = prDetail.Product,
                UnitOfMeasurement = prDetail.UnitOfMeasurement,
                QuantityInOrder = prDetail.QuantityInOrder,
                Subtotal = prDetail.Subtotal,
                PurchaseOrderHeader = poHeader

            };
            _unitOfWork.PurchaseOrderDetail.Add(poDetail);
        }

        // Final save to persist details
        _unitOfWork.Save();

    }
}