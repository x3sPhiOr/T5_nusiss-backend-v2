using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookStoreApi.ReservationApp.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
        // Navigation property
        public ICollection<Reservation> Reservations { get; set; }

        //public ICollection<Reservation> Reservations { get; set; }
    }
}
