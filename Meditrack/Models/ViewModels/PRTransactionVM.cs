using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Transactions;

namespace Meditrack.Models.ViewModels
{
    public class PRTransactionVM
    {
        [ValidateNever]
        public IEnumerable<Transaction> PRTransactionList { get; set; }
        public PurchaseRequisitionHeader PurchaseRequisitionHeader { get; set; }

        [ValidateNever]
        public PurchaseRequisitionDetail PurchaseRequisitionDetail { get; set; }

        [ValidateNever]
        public IEnumerable<PurchaseRequisitionDetail> PurchaseRequisitionDetailList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ProductList { get; set; }
    }
}
