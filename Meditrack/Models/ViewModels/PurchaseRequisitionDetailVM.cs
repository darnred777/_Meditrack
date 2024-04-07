using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Meditrack.Models.ViewModels
{
    public class PurchaseRequisitionDetailVM
    {

        public PurchaseRequisitionDetail PurchaseRequisitionDetail { get; set; }

		[ValidateNever]
        public IEnumerable<SelectListItem> HeaderIdList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ProductList { get; set; }
        
    }
}
