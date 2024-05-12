using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Meditrack.Models.ViewModels
{
    public class MonitoringVM
    {
        public Monitoring Monitoring { get; set; }

        public int ProductID { get; set; }
        public int SupplierID { get; set; }
        public int LocationID { get; set; }
        public int QuantityReceived { get; set; }
        public int QuantityExpected { get; set; }
        public int QuantityLacking { get; set; }
        public DateTime ReceivedDate { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ProductList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> SupplierList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> LocationList { get; set; }

    }

}

