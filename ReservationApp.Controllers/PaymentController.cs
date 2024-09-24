using BookStoreApi.ReservationApp.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.ReservationApp.Controllers
{
    public class PaymentController : ControllerBase
    {
        private readonly StripePaymentService _paymentService;
        private readonly ReservationContext _context;

        public PaymentController(StripePaymentService paymentService, ReservationContext context)
        {
            _paymentService = paymentService;
            _context = context;
        }

        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession()
        {
            var session = _paymentService.CreateCheckoutSession(
               "https://yourdomain.com/payment/success?sessionId={CHECKOUT_SESSION_ID}",
               "https://yourdomain.com/payment/cancel?sessionId={CHECKOUT_SESSION_ID}"
            );

            // Record the session in your DB
            _context.PaymentRecords.Add(new PaymentRecord
            {
                StripeSessionId = session.Id,
                Created = DateTime.UtcNow,
                Status = "Created"
            });
            _context.SaveChanges();

            return Ok(new { sessionId = session.Id });
        }

        [HttpGet("success")]
        public async Task<IActionResult> Success(string sessionId)
        {
            var paymentRecord = await _context.PaymentRecords.FirstOrDefaultAsync(p => p.StripeSessionId == sessionId);
            if (paymentRecord != null)
            {
                paymentRecord.Status = "Success";
                await _context.SaveChangesAsync();
            }

            // Redirect to a success page or return success response
            return Ok("Payment successful.");
        }

        [HttpGet("cancel")]
        public async Task<IActionResult> Cancel(string sessionId)
        {
            var paymentRecord = await _context.PaymentRecords.FirstOrDefaultAsync(p => p.StripeSessionId == sessionId);
            if (paymentRecord != null)
            {
                paymentRecord.Status = "Cancelled";
                await _context.SaveChangesAsync();
            }

            // Redirect to a cancel page or return cancel response
            return Ok("Payment cancelled.");
        }
    }
}
