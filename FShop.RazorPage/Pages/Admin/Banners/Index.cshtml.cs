using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Models;
using FShop.RazorPage.Models.Banners;
using FShop.RazorPage.Services.Banners;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Admin.Banners
{
    public class IndexModel : BaseRazorPage
    {
        public List<BannerDto>? Banners { get; set; }
        private readonly IBannerService _bannerService;

        public IndexModel(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }
        public async Task OnGet()
        {
            Banners = await _bannerService.GetList();
        }

        public async Task<IActionResult> OnPostDeleteBanner(long bannerId)
        {
            return await AjaxTryCatch(() =>
            {
                return  _bannerService.DeleteBanner(bannerId);
            });
        }
    }
}
