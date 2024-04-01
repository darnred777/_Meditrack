//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;

//namespace Meditrack.Models
//{
//    public class UserGroupMatrix
//    {
//        [Key]
//        public int UserGroupMatrixID { get; set; }

//        [Required]
//        public int UserID { get; set; }

//        [Required]
//        public int UserGroupID { get; set; }

//        [ForeignKey("UserID")]
//        public virtual User User { get; set; }

//        [ForeignKey("UserGroupID")]
//        public virtual UserGroup UserGroup { get; set; }
//    }
//}
