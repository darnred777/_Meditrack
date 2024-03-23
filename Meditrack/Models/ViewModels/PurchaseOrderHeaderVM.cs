using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Meditrack.Models.ViewModels
{
    public class PurchaseOrderHeaderVM
    {
        public PurchaseOrderHeader PurchaseOrderHeader { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> SupplierList { get; set; }
    }
}
