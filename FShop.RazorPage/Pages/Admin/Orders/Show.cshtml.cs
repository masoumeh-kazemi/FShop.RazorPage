using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Models.Orders;
using FShop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Admin.Orders
{
    public class ShowModel : BaseRazorPage
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
            if (order == null)
                return RedirectToPage("Index");

            Order = order;
            return Page();
        }

        public async Task<IActionResult> OnPost(long id)
        {
           var result = await _orderService.SendOrder(id);
           return RedirectAndShowAlert(result, RedirectToPage("Show",new {id}));
        }
    }
}
