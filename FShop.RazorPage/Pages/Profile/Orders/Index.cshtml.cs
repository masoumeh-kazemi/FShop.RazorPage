using FShop.RazorPage.Models.Orders;
using FShop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Profile.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public OrderFilterResult FilterResult { get; set; }
        public async Task OnGet(int pageId=1 ,OrderStatus? status = null)
        {
            FilterResult = await _orderService.GetUserOrders(pageId, 10, status);
        }
    }
}
