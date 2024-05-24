using FShop.RazorPage.Infrastructure;
using FShop.RazorPage.Models.Orders;
using FShop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Checkout
{
    public class FinallyOrderModel : PageModel
    {

        private IOrderService _orderService;

        public FinallyOrderModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public Order Order { get; set; }
        public async Task<IActionResult> OnGet(long orderId)
        {
            var order = await _orderService.GetOrderById(orderId);
            if (order == null || order.UserId != User.GetUserId())
                return Redirect("/");

            Order = order;
            return Page();
        }

        //private readonly IOrderService _orderService;

        //public FinallyOrderModel(IOrderService orderService)
        //{
        //    _orderService = orderService;
        //}
        //public Order Order { get; set; }
        //public async Task<IActionResult> OnGet(long orderId)
        //{
        //    var order = await _orderService.GetOrderById(orderId);

        //    if (order == null|| order.UserId != User.GetUserId())
        //    {
        //        return Redirect("/");
        //    }

        //    Order = order;
        //    return Page();
        //}
    }
}
