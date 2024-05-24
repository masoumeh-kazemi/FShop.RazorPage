using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using FShop.RazorPage.Models.Banners;
using FShop.RazorPage.Services.Banners;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Admin.Banners
{
    [BindProperties]
    public class AddModel : BaseRazorPage
    {
        public string Link { get; set; }
        //public string ImageName { get; set; }

        [FileImage]
        public IFormFile? ImageFile { get; set; }
        public BannerPosition Position { get; set; }

        private readonly IBannerService _bannerService;

        public AddModel(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            var result = await _bannerService.CreateBanner(new CreateBannerCommand()
            {
                Link = Link,
                ImageFile = ImageFile,
                Position = Position,
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
