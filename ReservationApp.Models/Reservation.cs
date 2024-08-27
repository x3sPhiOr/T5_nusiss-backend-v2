using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookStoreApi.ReservationApp.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public DateTime ReservationDate { get; set; }
        
        // Breakfast, Lunch, Afternoon Tea, Dinner
        public string Timing { get; set; }
        public int NumberOfTables { get; set; }
        public int NumberOfSeats { get; set; }

        // payment id , amount
        //public string Status { get; set; }

        public int CustomerID { get; set; }
        //[ForeignKey("CustomerID")]
        public Customer Customer { get; set; }

        //public ICollection<ReservationTable> ReservationTables { get; set; }
    }
}
