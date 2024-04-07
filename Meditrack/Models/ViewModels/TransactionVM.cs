namespace Meditrack.Models.ViewModels
{
    public class TransactionVM
    {
        public PurchaseRequisitionHeader PurchaseRequisitionHeader { get; set; }
        public IEnumerable<PurchaseRequisitionDetail> PurchaseRequisitionDetail { get; set; }
    }
}
