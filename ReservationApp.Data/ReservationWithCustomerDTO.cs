namespace BookStoreApi.ReservationApp.Data
{
    public class ReservationWithCustomerDTO
    {
        public int ReservationID { get; set; }
        public DateTime ReservationDate { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
