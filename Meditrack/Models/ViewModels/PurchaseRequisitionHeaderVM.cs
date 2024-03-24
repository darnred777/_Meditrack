using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Meditrack.Models.ViewModels
{
    public class PurchaseRequisitionHeaderVM
    {
        public PurchaseRequisitionHeader PurchaseRequisitionHeader { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> SupplierList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> LocationList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> StatusList { get; set; }
    }
}
