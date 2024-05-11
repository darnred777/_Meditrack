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
    
        public DateOnly? ReceivedDate { get; set; }
    }
}
