using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Meditrack.Models.ViewModels
{
    public class SupplierVM
    {
        public Supplier Supplier { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> LocationList { get; set; }
    }
}

