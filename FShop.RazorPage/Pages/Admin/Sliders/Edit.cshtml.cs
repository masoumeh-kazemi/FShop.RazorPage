using FShop.RazorPage.Services.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using FShop.RazorPage.Models.Sliders;

namespace FShop.RazorPage.Pages.Admin.Sliders
{
    [BindProperties]
    public class EditModel : BaseRazorPage
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Url)]
        public string Link { get; set; }

        [Display(Name = "عکس اسلایدر")]
        //[Required(ErrorMessage = "{0} را وارد کنید")]
        [FileImage(ErrorMessage = "عکس نامعتبر است")]
        public IFormFile? ImageFile { get; set; }

        //[Display(Name = "عکس")]
        ////[Required(ErrorMessage = "{0} را وارد کنید")]
        public string ImageName { get; set; }


        private readonly ISliderService _sliderService;

        public EditModel(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public async Task<IActionResult> OnGet(long id)
        {
            //var result = await _sliderService.GetSliderById(id);
            //Link = result.Link;
            //Title = result.Title;
            //ImageName = result.ImageName;
            //return Page();

            var slider = await _sliderService.GetSliderById(id);
            if (slider == null)
                return RedirectToPage("Index");


            Title = slider.Title;
            Link = slider.Link;
            ImageName = slider.ImageName;

            return Page();
        }


        public async Task<IActionResult> OnPost(long id)
        {
            var result = await _sliderService.EditSlider(new EditSliderCommand()
            {
                Id = id,
                ImageFile = ImageFile,
                Link = Link,
                Title = Title
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }


    }
}
