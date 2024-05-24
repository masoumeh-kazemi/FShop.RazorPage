using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Models;
using FShop.RazorPage.Models.Sellers;
using FShop.RazorPage.Models.Sellers.Command;
using FShop.RazorPage.Services.Sellers;
using FShop.RazorPage.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.SellerPanel.Inventories
{
    public class IndexModel : BaseRazorPage
    {
        public List<InventoryDto> Inventories { get; set; }

        private readonly ISellerService _sellerService;
        private IRenderViewToString _renderViewToString;

        public IndexModel(ISellerService sellerService, IRenderViewToString renderViewToString)
        {
            _sellerService = sellerService;
            _renderViewToString = renderViewToString;
        }
        public async Task<IActionResult> OnGet()
        {


            var currentSeller = await _sellerService.GetCurrentSeller();
            if (currentSeller == null)
                return Redirect("/");

            Inventories = await _sellerService.GetSellerInventories();
            return Page();

        }


        public async Task<IActionResult> OnGetEditPage(long id)
        {

            return await AjaxTryCatch(async () =>
            {
                var inventory = await _sellerService.GetInventoryById(id);
                if (inventory == null)
                    return ApiResult<string>.Success("اطلاعات نامعتبر است");

                var view = await _renderViewToString.RenderToStringAsync("_Edit", new EditSellerInventoryCommand
                {
                    SellerId = inventory.SellerId,
                    InventoryId = inventory.Id,
                    Count = inventory.Count,
                    Price = inventory.Price,
                    DiscountPercentage = inventory.DiscountPercentage
                }, PageContext);
                return ApiResult<string>.Success(view);
            });
        }

        public async Task<IActionResult> OnPost(EditSellerInventoryCommand command)
        {
            return await AjaxTryCatch(() => _sellerService.EditInventory(command));
        }
    }
}
