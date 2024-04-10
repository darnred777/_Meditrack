using System;
using Meditrack.Models;
using Meditrack.Repository.IRepository;

public class PurchaseOrderService : IPurchaseOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public PurchaseOrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void CreatePurchaseOrderFromRequisition(int prdId)
    {
        // Fetch the PurchaseRequisitionDetail by its ID
        var prDetail = _unitOfWork.PurchaseRequisitionDetail.Get(detail => detail.PRDtlID == prdId);


        if (prDetail == null)
        {
            throw new ArgumentException("Invalid Purchase Requisition Detail ID.");
        }

        // Create a new PurchaseOrderHeader
        var poHeader = new PurchaseOrderHeader
        {

            // Copy relevant information from PurchaseRequisitionHeader
            Supplier = prDetail.PurchaseRequisitionHeader.Supplier,
            Location = prDetail.PurchaseRequisitionHeader.Location,
            Status = prDetail.PurchaseRequisitionHeader.Status,
            PurchaseRequisitionHeader = prDetail.PurchaseRequisitionHeader,  
            PODate = DateTime.Now
        };

        // Add the PurchaseOrderHeader to the database
        _unitOfWork.PurchaseOrderHeader.Add(poHeader);
        _unitOfWork.Save();

        // Create a new PurchaseOrderDetail
        var poDetail = new PurchaseOrderDetail
        {
            POHdrID = poHeader.POHdrID, // Assign the newly generated PurchaseOrderHeader ID
            Product = prDetail.Product,
            UnitPrice = prDetail.UnitPrice,
            UnitOfMeasurement = prDetail.UnitOfMeasurement,
            QuantityInOrder = prDetail.QuantityInOrder,
            Subtotal = prDetail.Subtotal,
            PurchaseOrderHeader = poHeader
        };

        // Add the PurchaseOrderDetail to the database
        _unitOfWork.PurchaseOrderDetail.Add(poDetail);
        _unitOfWork.Save();

        // Optionally, you can mark the PurchaseRequisitionHeader and PurchaseRequisitionDetail as processed or delete them
        // For example:
        // _unitOfWork.PurchaseRequisitionHeader.Remove(prDetail.PurchaseRequisitionHeader);
        // _unitOfWork.PurchaseRequisitionDetail.Remove(prDetail);
        // _unitOfWork.Save();
    }
}
