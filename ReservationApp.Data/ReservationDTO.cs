using System.ComponentModel.DataAnnotations;
namespace BookStoreApi.ReservationApp.Data

{
    public class ReservationDTO
    {
        //public int ReservationID { get; set; }
        public DateTime ReservationDate { get; set; }

        // Breakfast, Lunch, Afternoon Tea, Dinner
        [Required(ErrorMessage = "Please select a timing option.")]
        public string Timing { get; set; }
        public int NumberOfTables { get; set; }
        public int NumberOfSeats { get; set; }

        public int CustomerID { get; set; }
    }
}
