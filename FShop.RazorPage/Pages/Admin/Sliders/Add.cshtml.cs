using System.ComponentModel.DataAnnotations;
using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Models.Sliders;
using FShop.RazorPage.Services.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Admin.Sliders
{
    [BindProperties]
    public class AddModel : BaseRazorPage
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Url)]
        public string Link { get; set; }

        [Display(Name = "عکس اسلایدر")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public IFormFile ImageFile { get; set; }

        private readonly ISliderService _sliderService;

        public AddModel(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            var result = await _sliderService.CreateSlider(new CreateSliderCommand()
            {
                Link = Link,
                Title = Title,
                ImageFile = ImageFile
            });

            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
