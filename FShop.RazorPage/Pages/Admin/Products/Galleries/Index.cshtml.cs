using System.ComponentModel.DataAnnotations;
using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using FShop.RazorPage.Models;
using FShop.RazorPage.Models.Products;
using FShop.RazorPage.Models.Products.Commands;
using FShop.RazorPage.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Admin.Products.Galleries
{
    public class IndexModel : BaseRazorPage
    {
        public List<ProductImageDto> Images { get; set; }


        [Display(Name = "نام تصویر")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string ImageName { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [FileImage(ErrorMessage = "عکس نامعتبر است")]
        [BindProperty]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "ترتیب نمایش")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [BindProperty]
        public int Sequence { get; set; }

        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> OnGet(long productId)
        {
            var product = await _productService.GetProductById(productId);
            if (product == null)
                RedirectToPage("Index");

            Images = product.Images;
            return Page();
        }

        public async Task<IActionResult> OnPost(long productId)
        {
             return await AjaxTryCatch( ()=>
            {
                return _productService.AddImageProduct(new AddImageProductCommand()
                {
                    ImageFile = ImageFile,
                    ProductId = productId,
                    Sequence = Sequence
                });
            });
        }
        public async Task<IActionResult> OnPostDeleteImageProduct(long productId, long id)
        {
            Sequence = 1;
            return await AjaxTryCatch(()
                => _productService.DeleteImageProduct(new DeleteImageProductCommand()
                    { ProductId = productId, ImageId = id }), checkModelState: false);
        }
    }
}
