namespace Meditrack.Models.ViewModels
{
    public class TransactionLogsVM
    {
        public int TransactionID { get; set; }
        public string TransType { get; set; }
        public string ApplicationUserId { get; set; }
        public string ApplicationUserEmail { get; set; }
        public string ApplicationUserFname{ get; set; }
        public string ApplicationUserLname{ get; set; }
        public string StatusDescription { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime TransDate { get; set; }
    }
}
