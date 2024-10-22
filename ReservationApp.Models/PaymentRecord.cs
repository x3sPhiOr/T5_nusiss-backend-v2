namespace BookStoreApi.ReservationApp.Models
{
    public class PaymentRecord
    {
        public int PaymentRecordID { get; set; }
        public string StripeSessionId { get; set; }
        public DateTime Created { get; set; }
        public string Status { get; set; }
    }
}
