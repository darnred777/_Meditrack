using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Meditrack.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        public int? LocationID { get; set; }

        [ForeignKey("LocationID")]
        [ValidateNever]
        public virtual Location Location { get; set; }

        [Required]
        [StringLength(30)]
        public string SupplierName { get; set; }

        [Required]
        [StringLength(30)]
        public string ContactPerson { get; set; }

        [Required]
        [StringLength(11)]
        public string ContactNumber { get; set; }

        [Required]
        [StringLength(30)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string OfficeAddress { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public bool isActive { get; set; } = true;
    }
}
