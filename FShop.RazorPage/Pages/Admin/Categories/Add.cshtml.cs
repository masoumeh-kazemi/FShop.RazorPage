using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Models;
using FShop.RazorPage.Models.Categories;
using FShop.RazorPage.Services.Categories;
using FShop.RazorPage.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Admin.Categories
{
    [BindProperties]
    public class AddModel : BaseRazorPage
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public SeoDataViewModel SeoData { get; set; }

        private readonly ICategoryService _categoryService;

        public AddModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost(long? parentId)
        {
            if (parentId == null)
            {
                var result = await _categoryService.CreateCategory(new CreateCategoryCommand()
                {
                    SeoData = SeoData.MapToSeoData(),
                    Slug = Slug,
                    Title = Title
                });

                return RedirectAndShowAlert(result, RedirectToPage("Index"));

            }

            var childCategory = await _categoryService.AddChildCategory(new AddChildCategoryCommand()
                {
                    SeoData = SeoData.MapToSeoData(),
                    Slug = Slug,
                    Title = Title,
                    ParentId = (long)parentId
                });

                return RedirectAndShowAlert(childCategory, RedirectToPage("Index"));

        }

    }
}
