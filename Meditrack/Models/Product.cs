using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Meditrack.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        [ValidateNever]
        public virtual ProductCategory ProductCategory { get; set; }


        [StringLength(30)]
        public string? ProductName { get; set; }


        [StringLength(15)]
        public string? SKU { get; set; }


        [StringLength(30)]
        public string? Brand { get; set; }


        public string? ProductDescription { get; set; }


        public decimal UnitPrice { get; set; }


        [StringLength(10)]
        public string? UnitOfMeasurement { get; set; }

 
        public int QuantityInStock { get; set; }

        public DateTime? ExpirationDate { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime LastUnitPriceUpdated { get; set; } = DateTime.Now;


        [DataType(DataType.DateTime)]
        public DateTime LastQuantityInStockUpdated { get; set; } = DateTime.Now;

   
        public bool isActive { get; set; } = true;
    }
}
