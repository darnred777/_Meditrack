using Meditrack.Models;
using Meditrack.Repository.IRepository;
using Meditrack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area("Approver")]
[Authorize(Roles = StaticDetails.Role_Approver)]
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

        //Preventing to Approve a Cancelled PR
        var statusCancelled = _unitOfWork.Status.GetFirstOrDefault(s => s.StatusDescription == StaticDetails.Status_Cancelled);
        if (prHeader.StatusID == statusCancelled.StatusID)
            throw new InvalidOperationException("Cannot approve a cancelled Purchase Requisition.");

        //Preventing to Approve an already Approved PR
        var myStatusApproved = _unitOfWork.Status.GetFirstOrDefault(s => s.StatusDescription == StaticDetails.Status_Approved);
        if (prHeader.StatusID == myStatusApproved.StatusID)
            throw new InvalidOperationException("Cannot approve a Purchase Requisition.");

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

    public void CancelPurchaseRequisition(int prdId)
    {
        var prHeader = _unitOfWork.PurchaseRequisitionHeader
                        .GetFirstOrDefault(prh => prh.PRHdrID == prdId);

        if (prHeader == null) throw new Exception("Purchase Requisition not found.");

        var statusCancelled = _unitOfWork.Status.GetFirstOrDefault(s => s.StatusDescription == StaticDetails.Status_Cancelled);
        if (statusCancelled == null) throw new Exception("Cancelled status not found in the database.");

        var statusApproved = _unitOfWork.Status.GetFirstOrDefault(s => s.StatusDescription == StaticDetails.Status_Approved);
        if (statusApproved == null)
            throw new Exception("Approved status not found in the database.");

        if (prHeader.StatusID == statusApproved.StatusID)
            throw new InvalidOperationException("Cannot cancel an already approved Purchase Requisition.");

        // Update the Status of the Purchase Requisition to 'Cancelled'
        prHeader.StatusID = statusCancelled.StatusID;
        _unitOfWork.Save(); // Save the status update
    }

    public void CancelPurchaseOrder(int poId)
    {
        // Retrieve the Purchase Order based on poId
        var purchaseOrder = _unitOfWork.PurchaseOrderHeader.GetFirstOrDefault(
            po => po.POHdrID == poId,
            includeProperties: "PurchaseRequisitionHeader.Status");

        if (purchaseOrder == null)
            throw new Exception("Purchase Order not found.");

        // Check if the status of the associated PR is already Cancelled
        if (purchaseOrder.PurchaseRequisitionHeader.Status.StatusDescription == StaticDetails.Status_Cancelled)
            throw new Exception("The associated Purchase Requisition is already cancelled.");

        // Retrieve the Cancelled status from the database
        var statusCancelled = _unitOfWork.Status.GetFirstOrDefault(s => s.StatusDescription == StaticDetails.Status_Cancelled);
        if (statusCancelled == null)
            throw new Exception("Cancelled status not found in the database.");

        // Update the status of the Purchase Order to Cancelled
        purchaseOrder.Status = statusCancelled;
        purchaseOrder.StatusID = statusCancelled.StatusID;

        // Also update the status of the associated Purchase Requisition
        var prHeader = purchaseOrder.PurchaseRequisitionHeader;
        prHeader.Status = statusCancelled;
        prHeader.StatusID = statusCancelled.StatusID;

        // Save changes
        _unitOfWork.Save();
    }
}