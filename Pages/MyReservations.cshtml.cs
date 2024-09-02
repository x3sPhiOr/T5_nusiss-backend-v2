using BookStoreApi.ReservationApp.Data;
using BookStoreApi.ReservationApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace BookStoreApi.Views
{
    public class ReservationModel : PageModel
    {

        private readonly HttpClient _httpClient;

        public ReservationModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7248/");

        }

        public List<Reservation> Reservations { get; set; }

        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetStringAsync("https://localhost:7248/reservations");
            Reservations = JsonSerializer.Deserialize<List<Reservation>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
