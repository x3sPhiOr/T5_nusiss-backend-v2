namespace BookStoreApi.ReservationApp.Models
{
    public class ReservationTable
    {
        public int ReservationTableID { get; set; }
        public int ReservationID { get; set; }
        public Reservation Reservation { get; set; }

        public int TableID { get; set; }
        public Table Table { get; set; }
    }
}
