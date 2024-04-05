using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Meditrack.Models
{
    public class PurchaseOrderHeader
    {
        [Key]
        public int POHdrID { get; set; }

        [Required]
        public int StatusID { get; set; }

        [ForeignKey("StatusID")]
        [ValidateNever]
        public virtual required Status Status { get; set; }

        [Required]
        public int SupplierID { get; set; }

        [ForeignKey("SupplierID")]
        public virtual required Supplier Supplier { get; set; }

        [Required]
        public int LocationID { get; set; }

        [ForeignKey("LocationID")]
        public virtual required Location Location { get; set; }

        [Required]
        public int PRHdrID { get; set; }

        [ForeignKey("PRHdrID")]
        public virtual required PurchaseRequisitionHeader PurchaseRequisitionHeader { get; set; }
        public decimal TotalAmount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PODate { get; set; } = DateTime.Now;

        [MaxLength(255)]
        public string? Remarks { get; set; }
    }
}
