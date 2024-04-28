using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Meditrack.Models
{
    public class TransactionLogs
    {
        [Key]
        public int TransactionID { get; set; }

        [Required]
        [StringLength(5)]
        public required string TransType { get; set; }

        [ValidateNever]
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        [ValidateNever]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int? StatusID { get; set; }

        [ForeignKey("StatusID")]
        [ValidateNever]
        public virtual Status? Status { get; set; }

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
