using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStoreApi.Pages
{
    public class RestarauntsModel : PageModel
    {
        private readonly ILogger<RestarauntsModel> _logger;

        public RestarauntsModel(ILogger<RestarauntsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }

}
