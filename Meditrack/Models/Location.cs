using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Meditrack.Models
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }

        [StringLength(30)]
        [Display(Name = "Location Type")]
        public string? LocationType { get; set; }

        [StringLength(30)]
        [Display(Name = "Location Address")]
        public string? LocationAddress { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date/Time")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
