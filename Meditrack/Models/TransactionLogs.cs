using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Meditrack.Models
{
    public class TransactionLogs
    {
        [Key]
        public int TransactionID { get; set; }

        [StringLength(5)]
        public string? TransType { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public int? StatusID { get; set; }

        [ForeignKey("StatusID")]
        public virtual Status Status { get; set; }

        public int? POHdrID { get; set; }

        [ForeignKey("POHdrID")]
        public virtual PurchaseOrderHeader PurchaseOrderHeader { get; set; }

        public int? PRHdrID { get; set; }

        [ForeignKey("PRHdrID")]
        public virtual PurchaseRequisitionHeader PurchaseRequisitionHeader { get; set; }

        public int? ProductID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        public int? Quantity { get; set; }

        [Required]
        public DateTime TransDate { get; set; } = DateTime.Now;
    }
}
