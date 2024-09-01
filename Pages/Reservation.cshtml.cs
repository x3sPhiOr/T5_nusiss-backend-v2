using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStoreApi.ReservationApp.Models;

namespace BookStoreApi.Views
{
    public class ReservationModel : PageModel
    {
        private readonly BookStoreApi.ReservationApp.Models.ReservationContext _context;

        public ReservationModel(BookStoreApi.ReservationApp.Models.ReservationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "CustomerID");
            return Page();
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
