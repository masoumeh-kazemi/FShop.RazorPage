using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Infrastructure.Utils;
using FShop.RazorPage.Models.Orders;
using FShop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Admin.Orders
{
    public class IndexModel : BaseRazorFilter<OrderFilterParams>
    {
        private readonly IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public OrderFilterResult FilterResult { get; set; }
        public async Task OnGet(string? startDate, string? endDate)
        {
            if (string.IsNullOrWhiteSpace(startDate) == false)
            {
                FilterParams.StartDate = startDate.ToMiladi();
            }

            if (string.IsNullOrWhiteSpace(endDate) == false)
            {
                FilterParams.EndDate = endDate.ToMiladi();
            }

            FilterParams.Take = 1;
            FilterResult = await _orderService.GetOrderFilter(FilterParams);
        }
    }
}
