using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Meditrack.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Selecting a Category is required.")]
        public int CategoryID { get; set; }

        [Required]
        [ForeignKey("CategoryID")]
        [ValidateNever]
        public virtual ProductCategory ProductCategory { get; set; }

        [Required]
        [StringLength(30)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(15)]
        public string SKU { get; set; }

        [Required]
        [StringLength(30)]
        public string Brand { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        [StringLength(10)]
        public string UnitOfMeasurement { get; set; }

        [Required]
        public int QuantityInStock { get; set; }

        [Required]
        public DateOnly ExpirationDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastUnitPriceUpdated { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastQuantityInStockUpdated { get; set; } = DateTime.Now;

        [Required]
        public bool isActive { get; set; } = true;
    }
}
