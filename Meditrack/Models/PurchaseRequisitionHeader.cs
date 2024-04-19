using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Meditrack.Models
{
    public class PurchaseRequisitionHeader
    {
        [Key]
        public int PRHdrID { get; set; }

        [Required(ErrorMessage = "Selecting a Supplier is required.")]
        public int SupplierID { get; set; }

        [Required(ErrorMessage = "Selecting a Supplier is required.")]
        [ForeignKey("SupplierID")]
        [ValidateNever]
        public virtual Supplier Supplier { get; set; }

        [Required(ErrorMessage = "Selecting a Location is required.")]
        [ValidateNever]
        public int LocationID { get; set; }

        [Required(ErrorMessage = "Selecting a Location is required.")]
        [ForeignKey("LocationID")]
        [ValidateNever]
        public virtual Location Location { get; set; }

        public int? StatusID { get; set; }

        [ForeignKey("StatusID")]
        [ValidateNever]
        public virtual Status? Status { get; set; }

        public decimal? TotalAmount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PRDate { get; set; } = DateTime.Now;

        public virtual ICollection<PurchaseRequisitionDetail> PurchaseRequisitionDetail { get; set; }
    }
}
