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
        public string UserGroupName { get; set; }
    }

    public class Status
    {
        [Key]
        public int StatusID { get; set; }

        [Required]
        public string StatusDescription { get; set; }

    }

    public class Location
    {
        [Key]
        public int LocationID { get; set; }

        [Required]
        [StringLength(30)]
        public string LocationType { get; set; }

        [Required]
        [StringLength(30)]
        public string LocationAddress { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }

    public class ProductCategory
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(30)]
        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        [Required]
        public DateTime DateLastModified { get; set; } = DateTime.Now;

        public int TotalQuantityInStock { get; set; }
    }

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

        public byte[] ProfilePicture { get; set; }

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
        public virtual Location Location { get; set; }
    }

    public class UserGroupMatrix
    {
        [Key]
        public int UserGroupMatrixID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int UserGroupID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [ForeignKey("UserGroupID")]
        public virtual UserGroup UserGroup { get; set; }
    }

    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        public int? LocationID { get; set; }

        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }

        [Required]
        [StringLength(30)]
        public string SupplierName { get; set; }

        [Required]
        [StringLength(30)]
        public string ContactPerson { get; set; }

        [Required]
        [StringLength(11)]
        public string ContactNumber { get; set; }

        [Required]
        [StringLength(30)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string OfficeAddress { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public bool isActive { get; set; } = true;
    }

    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategory { get; set; }

        [Required]
        [StringLength(30)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(15)]
        public string SKU { get; set; }

        [Required]
        [StringLength(30)]
        public string Brand { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        [StringLength(10)]
        public string UnitOfMeasurement { get; set; }

        [Required]
        public int QuantityInStock { get; set; }

        public DateTime? ExpirationDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastUnitPriceUpdated { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastQuantityInStockUpdated { get; set; } = DateTime.Now;

        [Required]
        public bool isActive { get; set; } = true;
    }

    public class PurchaseRequisitionHeader
    {
        [Key]
        public int PRHdrID { get; set; }

        [Required]
        public int SupplierID { get; set; }

        [ForeignKey("SupplierID")]
        public virtual Supplier Supplier { get; set; }

        [Required]
        public int LocationID { get; set; }

        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }

        [Required]
        public int StatusID { get; set; }

        [ForeignKey("StatusID")]
        public virtual Status Status { get; set; }

        [Required]
        [Column(TypeName = "MONEY")]
        public decimal TotalAmount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PRDate { get; set; } = DateTime.Now;
    }


    public class PurchaseRequisitionDetail
    {
        [Key]
        public int PRDtlID { get; set; }

        [Required]
        public int PRHdrID { get; set; }

        [ForeignKey("PRHdrID")]
        public virtual PurchaseRequisitionHeader PurchaseRequisitionHeader { get; set; }

        public int? ProductID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [StringLength(10)]
        public string UnitOfMeasurement { get; set; }

        [Required]
        public int QuantityInOrder { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Subtotal { get; set; }
    }

    public class PurchaseOrderHeader
    {
        [Key]
        public int POHdrID { get; set; }

        [Required]
        public int StatusID { get; set; }

        [ForeignKey("StatusID")]
        public virtual Status Status { get; set; }

        [Required]
        public int SupplierID { get; set; }

        [ForeignKey("SupplierID")]
        public virtual Supplier Supplier { get; set; }

        [Required]
        public int LocationID { get; set; }

        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }

        [Required]
        public int PRHdrID { get; set; }

        [ForeignKey("PRHdrID")]
        public virtual PurchaseRequisitionHeader PurchaseRequisitionHeader { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PODate { get; set; } = DateTime.Now;

        [MaxLength(255)]
        public string Remarks { get; set; }
    }

    public class PurchaseOrderDetail
    {
        [Key]
        public int PODtlID { get; set; }

        [Required]
        public int POHdrID { get; set; }

        [ForeignKey("POHdrID")]
        public PurchaseOrderHeader PurchaseOrderHeader { get; set; }

        [Required]
        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public Product Product { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        [StringLength(10)]
        public string UnitOfMeasurement { get; set; }

        [Required]
        public int QuantityInOrder { get; set; }

        [NotMapped]
        public decimal Subtotal => UnitPrice * QuantityInOrder;

        [Required]
        public bool IsVATExclusive { get; set; } = true;
    }

    public class TransactionLogs
    {
        [Key]
        public int TransactionID { get; set; }

        [Required]
        [StringLength(5)]
        public string TransType { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [Required]
        public int StatusID { get; set; }

        [ForeignKey("StatusID")]
        public Status Status { get; set; }

        public int? POHdrID { get; set; }

        [ForeignKey("POHdrID")]
        public PurchaseOrderHeader PurchaseOrderHeader { get; set; }

        public int? PRHdrID { get; set; }

        [ForeignKey("PRHdrID")]
        public PurchaseRequisitionHeader PurchaseRequisitionHeader { get; set; }

        public int? ProductID { get; set; }

        [ForeignKey("ProductID")]
        public Product Product { get; set; }

        public int? Quantity { get; set; }

        [Required]
        public DateTime TransDate { get; set; } = DateTime.Now;
    }
}

