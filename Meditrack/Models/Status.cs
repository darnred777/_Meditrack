using System.ComponentModel.DataAnnotations;

namespace Meditrack.Models
{
    public class Status
    {
        [Key]
        public int StatusID { get; set; }

        [Display(Name = "Status Description")]
        public string? StatusDescription { get; set; }

    }
}
