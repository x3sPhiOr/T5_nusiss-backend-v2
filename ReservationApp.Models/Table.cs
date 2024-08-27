namespace BookStoreApi.ReservationApp.Models
{
    public class Table
    {
        public int TableID { get; set; }
        public int TableNumber { get; set; }
        public int NumberOfSeats { get; set; }
        public string Location { get; set; }

        //public ICollection<ReservationTable> ReservationTables { get; set; }
    }
}
