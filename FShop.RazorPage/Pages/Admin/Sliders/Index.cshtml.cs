using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Models.Sliders;
using FShop.RazorPage.Services.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Admin.Sliders
{
    [BindProperties]
    public class IndexModel : BaseRazorPage
    {
        #region Properties


        public List<SliderDto> Sliders { get; set; }
        #endregion

        private readonly ISliderService _sliderService;

        public IndexModel(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public async Task OnGet()
        {
             Sliders = await _sliderService.GetSliders();
            
        }


        public async Task<IActionResult> OnPostDeleteSlider(long sliderId)
        {
            return await AjaxTryCatch(() =>
            {
                return _sliderService.DeleteSlider(sliderId);
            });
        }
    }
}
