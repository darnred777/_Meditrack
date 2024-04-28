using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Meditrack.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ProductCategoryList { get; set; }

        public int QuantityChange { get; set; }
    }
}
