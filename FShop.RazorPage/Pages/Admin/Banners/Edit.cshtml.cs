using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using FShop.RazorPage.Models.Banners;
using FShop.RazorPage.Services.Banners;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Admin.Banners
{
    [BindProperties]
    public class EditModel : BaseRazorPage
    {
        public string Link { get; set; }
        public string? ImageName { get; set; }
        public BannerPosition Position { get; set; }

        [FileImage]
        public IFormFile ImageFile { get; set; }
        
        private readonly IBannerService _bannerService;

        public EditModel(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }
        public async Task OnGet(long id)
        {
            var banner = await _bannerService.GetBannerById(id);
            Link = banner.Link;
            ImageName = banner.ImageName;
            Position =  banner.Position;
        }

        public async Task<IActionResult> OnPost(long id)
        {
            var result = await _bannerService.EditBanner(new EditBannerCommand()
            {
                ImageFile = ImageFile,
                Link = Link,
                Position = Position,
                Id = id,
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
