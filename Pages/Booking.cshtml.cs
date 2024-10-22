using BookStoreApi.ReservationApp.Data;
using BookStoreApi.ReservationApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace BookStoreApi.Views
{
    public class BookingModel : PageModel
    {

        private readonly HttpClient _httpClient;

        public BookingModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7248/");

            TimingOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "Breakfast", Text = "Breakfast" },
                new SelectListItem { Value = "Lunch", Text = "Lunch" },
                new SelectListItem { Value = "Dinner", Text = "Dinner" },
            };

        }

        //[BindProperty]
        //public Reservation reservation { get; set; }
        [BindProperty]
        public ReservationDTO NewReservation { get; set; }

        public List<Reservation> Reservations { get; set; }

        public List<SelectListItem> TimingOptions { get; set; }

        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetStringAsync("https://localhost:7248/reservations");
            Reservations = JsonSerializer.Deserialize<List<Reservation>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            //TimingOptions = new List<SelectListItem>
            //{
            //    new SelectListItem { Value = "Breakfast", Text = "Breakfast" },
            //    new SelectListItem { Value = "Lunch", Text = "Lunch" },
            //    new SelectListItem { Value = "Dinner", Text = "Dinner" },
            //};
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var response1 = await _httpClient.GetStringAsync("https://localhost:7248/reservations");
            Reservations = JsonSerializer.Deserialize<List<Reservation>>(response1, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (!ModelState.IsValid)
            {
                return Page();
            }

            //var response = await _httpClient.PostAsJsonAsync("Reservations/v2", reservation);
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7248/Reservations/v2", NewReservation);

            if (response.IsSuccessStatusCode)
            {
                // Handle success (e.g., redirect to a confirmation page)
                return RedirectToPage();
            }
            else
            {
                // Handle error (e.g., display error message)
                ModelState.AddModelError(string.Empty, "An error occurred while saving the reservation.");
                return Page();
            }
        }
    }
}
