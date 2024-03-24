using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meditrack.Models
{
    public class ProductCategory
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Product Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }

        [Required]
        [Display(Name = "Date Last Modified")]
        public DateTime DateLastModified { get; set; } = DateTime.Now;

        [Display(Name = "Total Quantity In Stock")]
        [NotMapped]
        public int TotalQuantityInStock { get; set; }
    }
}
