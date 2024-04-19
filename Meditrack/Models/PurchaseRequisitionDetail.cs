using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Meditrack.Models
{
    public class PurchaseRequisitionDetail
    {
        [Key]
        public int PRDtlID { get; set; }

        [Required]
        public int PRHdrID { get; set; }

        [ForeignKey("PRHdrID")]
        [ValidateNever]
        public virtual PurchaseRequisitionHeader PurchaseRequisitionHeader { get; set; }

        [Required(ErrorMessage = "Selecting a Product is required.")]
        public int? ProductID { get; set; }

        [Required(ErrorMessage = "Selecting a Product is required.")]
        [ForeignKey("ProductID")]
        [ValidateNever]
        public virtual Product Product { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [StringLength(10)]
        public string UnitOfMeasurement { get; set; }

        [Range(1, 1000000)]
        public int QuantityInOrder { get; set; }

        public decimal Subtotal { get; set; }

        //[NotMapped]
        //public string myStatus { get; set; }

        //[NotMapped]
        //public decimal Total { get; set; }
     
    }
}
