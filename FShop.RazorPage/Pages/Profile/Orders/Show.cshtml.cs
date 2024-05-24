using FShop.RazorPage.Infrastructure;
using FShop.RazorPage.Models.Orders;
using FShop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Profile.Orders
{

    public class ShowModel : PageModel
    {
        private readonly IOrderService _orderService;

        public ShowModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public Order Order { get; set; }
        public async Task<IActionResult> OnGet(long id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null || order.UserId != User.GetUserId())
            {
                return Redirect("Index");
            }

            Order = order;
            return Page();
        }
    }
}
