using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.ReservationApp.Models
{
    public class BuffetTable
    {
        [Key]
        public int TableID { get; set; }
        public int TableNumber { get; set; }
        public int NumberOfSeats { get; set; }
        public string Location { get; set; }
        // Navigation property
        public ICollection<ReservationTable> ReservationTables { get; set; }
    }
}
