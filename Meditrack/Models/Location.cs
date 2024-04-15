using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Meditrack.Models
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Location Type")]
        public string? LocationType { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Location Address")]
        public string? LocationAddress { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date/Time")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
