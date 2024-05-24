using System.ComponentModel.DataAnnotations;
using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using FShop.RazorPage.Models;
using FShop.RazorPage.Models.Products.Commands;
using FShop.RazorPage.Services.Products;
using FShop.RazorPage.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Admin.Products
{
    [BindProperties]
    public class EditModel : BaseRazorPage
    {



        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Title { get;  set; }

        [Display(Name = "slug")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Slug { get; set; }

        [Display(Name = "عکس محصول")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [FileImage(ErrorMessage = "عکس نامعتبر است")]
        public IFormFile? ImageFile { get;  set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [UIHint("Ckeditor4")]
        public string Description { get; set; }

        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public long CategoryId { get; set; }

        [Display(Name = "زیردسته اول")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public long SubCategoryId { get; set; }

        [Display(Name = "زیردسته دوم")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public long? SecondarySubCategoryId { get; set; }

        [Display(Name = "SeoData")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public SeoDataViewModel SeoData { get; set; }
        public List<string> Keys { get; set; }
        public List<string> Values { get; set; }

        //public Dictionary<string, string> Specifications { get;  set; }

        private readonly IProductService _productService;

        public EditModel(IProductService productService)
        {
            _productService = productService;
        }
        public async Task OnGet(long id)
        {
            var product = await _productService.GetProductById(id);
            Title = product.Title;
            Slug = product.Slug;
            Description = product.Description;
            CategoryId = product.Category.Id;
            SubCategoryId = product.SubCategory.Id;
            SecondarySubCategoryId = product.SecondarySubCategory?.Id;
            SeoData = SeoDataViewModel.MapSeoDataToViewModel(product.SeoData);
            Keys = product.Specifications.Select(x => x.Key).ToList();
            Values = product.Specifications.Select(f => f.Value).ToList();
        }

        public async Task<IActionResult> OnPost(long id)
        {
            var result = await _productService.EditProduct(new EditProductCommand()
            {
                ProductId = id,
                Title = Title,
                Slug = Slug,
                Description = Description,
                CategoryId = CategoryId,
                SubCategoryId = SubCategoryId,
                SecondarySubCategoryId = (long)SecondarySubCategoryId,
                SeoData = SeoData.MapToSeoData(),
                Specifications = ConvertToDictionary(),
                ImageFile = ImageFile
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }

        private Dictionary<string, string> ConvertToDictionary()
        {
            var specification = new Dictionary<string, string>();
            for (int i = 0; i < Keys.Count; i++)
            {
                specification.Add(Keys[i], Values[i]);
            }
            return specification;
        }
    }
}
