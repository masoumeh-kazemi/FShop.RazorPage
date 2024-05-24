using FShop.RazorPage.Models.Sellers;
using FShop.RazorPage.Services.Sellers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.SellerPanel
{
    public class IndexModel : PageModel
    {

        private readonly ISellerService _sellerService;

        public IndexModel(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public SellerDto? Seller { get; set; }
 
        public async Task OnGet()
        {
            Seller = await _sellerService.GetCurrentSeller();
        }
    }
}
