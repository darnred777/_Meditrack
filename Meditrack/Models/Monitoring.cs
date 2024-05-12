using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Meditrack.Models
{
    public class Monitoring
    {
        [Key]
        public int MonitoringID { get; set; }

        [ValidateNever]
        [Required(ErrorMessage = "Selecting a Product is required.")]
        public int ProductID { get; set; }

        [ValidateNever]
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [Required]
        [Range(1, 1000000)]
        public int QuantityReceived { get; set; }

        [Required]
        [Range(1, 1000000)]
        public int QuantityExpected { get; set; }

        public int? QuantityLacking { get; set; }

        [ValidateNever]
        [Required(ErrorMessage = "Selecting a Supplier is required.")]
        public int SupplierID { get; set; }

        [ValidateNever]
        [ForeignKey("SupplierID")]
        public virtual Supplier Supplier { get; set; }

        [Required(ErrorMessage = "Selecting a Location is required.")]
        [ValidateNever]
        public int LocationID { get; set; }

        [Required(ErrorMessage = "Selecting a Location is required.")]
        [ForeignKey("LocationID")]
        [ValidateNever]
        public virtual Location Location { get; set; }

        public DateOnly? ReceivedDate { get; set; }
    }
}
