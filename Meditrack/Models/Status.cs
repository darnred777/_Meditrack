using System.ComponentModel.DataAnnotations;

namespace Meditrack.Models
{
    public class Status
    {
        [Key]
        public int StatusID { get; set; }

        [Required]
        [Display(Name = "Status Description")]
        public string? StatusDescription { get; set; }

    }
}
