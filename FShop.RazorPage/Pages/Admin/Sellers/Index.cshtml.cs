using FShop.RazorPage.Models.Sellers;
using FShop.RazorPage.Services.Sellers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Admin.Sellers
{
    public class IndexModel : PageModel
    {
        private readonly ISellerService _sellerService;

        public IndexModel(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public SellerFilterResult Sellers { get; set; }
        public async Task OnGet(string nationalCode="", string shopName="", int pageId=1, int take = 10)
        {
            Sellers = await _sellerService.GetSellersByFilter(new SellerFilterParams()
            {
                NationalCode = nationalCode,
                ShopName = shopName,
                Take = take,
                PageId = pageId,

            });
        }
    }
}
