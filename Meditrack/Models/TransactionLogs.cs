using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Meditrack.Models
{
    public class TransactionLogs
    {
        [Key]
        public int TransactionID { get; set; }

        [Required]
        [StringLength(5)]
        public required string TransType { get; set; }

        [Required]
        public string Id { get; set; }

        [ForeignKey("Id")]
        public required ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int StatusID { get; set; }

        [ForeignKey("StatusID")]
        public required Status Status { get; set; }

        public int? POHdrID { get; set; }

        [ForeignKey("POHdrID")]
        public required PurchaseOrderHeader PurchaseOrderHeader { get; set; }

        public int? PRHdrID { get; set; }

        [ForeignKey("PRHdrID")]
        public required PurchaseRequisitionHeader PurchaseRequisitionHeader { get; set; }

        public int? ProductID { get; set; }

        [ForeignKey("ProductID")]
        public required Product Product { get; set; }

        public int? Quantity { get; set; }

        [Required]
        public DateTime TransDate { get; set; } = DateTime.Now;
    }
}
