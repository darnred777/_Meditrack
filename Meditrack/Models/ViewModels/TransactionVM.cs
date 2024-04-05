using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Transactions;

namespace Meditrack.Models.ViewModels
{
    public class TransactionVM
    {      
        [ValidateNever]
        public IEnumerable<Transaction> TransactionList { get; set; }
        public PurchaseRequisitionHeader PurchaseRequisitionHeader { get; set; }
    }
}
