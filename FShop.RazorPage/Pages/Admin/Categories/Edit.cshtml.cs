using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Models;
using FShop.RazorPage.Models.Categories;
using FShop.RazorPage.Services.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Admin.Categories
{

    [BindProperties]
    public class EditModel : BaseRazorPage
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public SeoData SeoData { get; set; }

        private readonly ICategoryService _categoryService;

        public EditModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task OnGet(long id)
        {
            var result = await _categoryService.GetCategoryById(id);
            Title = result.Title;
            Slug = result.Slug;
            SeoData = result.SeoData;
        }

        public async Task<IActionResult> OnPost(long id)
        {
            var result = await _categoryService.EditCategory(new EditCategoryCommand()
            {
                Id = id,
                SeoData = SeoData,
                Title = Title,
                Slug = Slug
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }

    }
}
