using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Meditrack.Models
{
    public class PurchaseRequisitionDetail
    {
        [Key]
        public int PRDtlID { get; set; }

        [Required]
        public int PRHdrID { get; set; }

        [ForeignKey("PRHdrID")]
        public virtual PurchaseRequisitionHeader PurchaseRequisitionHeader { get; set; }

        public int? ProductID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [StringLength(10)]
        public string UnitOfMeasurement { get; set; }

        public int QuantityInOrder { get; set; }

        [Required]
        public decimal Subtotal { get; set; }
    }
}
