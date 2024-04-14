public interface IPurchaseOrderService
{
    void CreatePurchaseOrderFromRequisition(int prdId);
    void CancelPurchaseRequisition(int prdId);
    void CancelPurchaseOrder(int prdId);
}