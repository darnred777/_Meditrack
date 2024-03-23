using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Meditrack.Models.ViewModels
{
    public class PurchaseRequisitionHeaderVM
    {
        public PurchaseRequisitionHeader PurchaseRequisitionHeader { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> SupplierList { get; set; }
        public IEnumerable<SelectListItem> LocationList { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
    }
}
