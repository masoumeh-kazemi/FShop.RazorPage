using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Models.Categories;
using FShop.RazorPage.Services.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Admin.Categories
{
    public class IndexModel : BaseRazorPage
    {
        public List<CategoryDto> Categories { get; set; }
        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task OnGet()
        {
            Categories = await _categoryService.GetCategories();
        }

        public async Task<IActionResult> OnPostDeleteItem(long categoryId)
        {
            return await AjaxTryCatch(() =>
            {
               return  _categoryService.DeleteCategory(categoryId);
            });
        }
    }
}
