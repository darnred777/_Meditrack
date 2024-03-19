using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meditrack.Models
{
    public class UserGroup
    {
        [Key]
        public int UserGroupID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name="User Group Name")]
        public string UserGroupName { get; set; }
    }
}

