using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Meditrack.Models
{
    public class PurchaseOrderDetail
    {
        [Key]
        public int PODtlID { get; set; }

        [Required]
        public int POHdrID { get; set; }

        [ForeignKey("POHdrID")]
        public required PurchaseOrderHeader PurchaseOrderHeader { get; set; }

        [Required]
        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public required Product Product { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        [StringLength(10)]
        public required string UnitOfMeasurement { get; set; }

        [Required]
        public int QuantityInOrder { get; set; }

        [NotMapped]
        public decimal Subtotal => UnitPrice * QuantityInOrder;

        [Required]
        public bool IsVATExclusive { get; set; } = true;
    }
}
