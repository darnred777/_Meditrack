using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Meditrack.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public int LocationID { get; set; }

        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(30)]
        public string Password { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }

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

        [Required]
        [ForeignKey("LocationID")]
        [ValidateNever]
        public virtual Location Location { get; set; }
    }
}
