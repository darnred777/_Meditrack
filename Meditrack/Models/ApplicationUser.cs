using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meditrack.Models
{
    public class ApplicationUser: IdentityUser
    {
        public int LocationID { get; set; }

        [Required]
        [ForeignKey("LocationID")]
        [ValidateNever]
        public virtual Location Location { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        [ValidateNever]
        public string? ProfilePicture { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastLoginTime_Date { get; set; } = DateTime.Now;

        [Required]
        public bool IsActive { get; set; } = true;
    }
}
