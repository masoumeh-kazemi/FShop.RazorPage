using FShop.RazorPage.Infrastructure;
using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Models.Comments;
using FShop.RazorPage.Models.Products;
using FShop.RazorPage.Models.Sellers;
using FShop.RazorPage.Services.Comments;
using FShop.RazorPage.Services.Products;
using FShop.RazorPage.Services.Sellers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages
{
    public class ProductModel : BaseRazorPage
    {
        public SingleProductDto ProductPageModel { get; set; }
        private readonly IProductService _productService;
        private readonly ICommentService _commentService;
        public ProductModel(IProductService productService,ICommentService commentService)
        {
            _productService = productService;
            _commentService = commentService;
        }
        public async Task<IActionResult> OnGet(string slug)
        {
            var product = await _productService.GetSingleProduct(slug);
            if (product==null)
                return NotFound();

            ProductPageModel = product;

            return Page();

        }

        public async Task<IActionResult> OnPost(long productId, string comment, string slug)
        {
            if (!User.Identity.IsAuthenticated)
                return Page();

            var result = await _commentService.AddComment(new AddCommentCommand()
            {
                ProductId = productId,
                Text = comment,
                UserId = User.GetUserId()
            });
            SuccessAlert("نظر شما ثبت شد و پس از تایید در سایت به نمایش در می آید");
            return RedirectToPage("Product", new { slug = slug });
        }
        public async Task<IActionResult> OnGetProductComments(long productId, int pageId=1)
        {
            var commentResult = await _commentService.GetProductComments(pageId,1,productId);
            return Partial("Shared/Products/_Comments", commentResult);
        }



        public async Task<IActionResult> OnPostDeleteProductComment(long commentId)
        {
            return await AjaxTryCatch(() =>
            {
                return  _commentService.DeleteComment(commentId);
            });
        }

    }
}
