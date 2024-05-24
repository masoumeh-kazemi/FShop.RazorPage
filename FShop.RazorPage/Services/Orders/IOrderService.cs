using FShop.RazorPage.Models;
using FShop.RazorPage.Models.Orders;
using FShop.RazorPage.Models.Orders.Command;

namespace FShop.RazorPage.Services.Orders;

public interface IOrderService
{
    Task<ApiResult> AddOrderItem(AddOrderItemCommand command);
    Task<ApiResult> CheckoutOrderItem(CheckoutOrderCommand command);
    Task<ApiResult> SendOrder(long orderId);

    Task<ApiResult> IncreaseOrderItemCount(IncreaseOrderItemCountCommand command);
    Task<ApiResult> DecreaseOrderItemCount(DecreaseOrderItemCountCommand command);
    Task<ApiResult> DeleteOrderItem(DeleteOrderItemCommand command);


    Task<Order?> GetOrderById(long orderId);
    Task<OrderFilterResult> GetOrderFilter(OrderFilterParams filterParams);
    Task<Order?> GetCurrentOrder();
    Task<OrderFilterResult> GetUserOrders(int pageId, int take, OrderStatus? status);

}


public class OrderService : IOrderService
{
private readonly HttpClient _httpClient;

public OrderService(HttpClient httpClient)
{
    _httpClient = httpClient;
}
public async Task<ApiResult> AddOrderItem(AddOrderItemCommand command)
{
    var result = await _httpClient.PostAsJsonAsync("order", command);
    return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> CheckoutOrderItem(CheckoutOrderCommand command)
    {
        var result = await _httpClient.PostAsJsonAsync("order/checkout", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> SendOrder(long orderId)
    {
        var result = await _httpClient.PutAsJsonAsync($"order/sendOrder/{orderId}", new object());
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> IncreaseOrderItemCount(IncreaseOrderItemCountCommand command)
    {
        var result = await _httpClient.PutAsJsonAsync("order/orderItem/increaseCount", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DecreaseOrderItemCount(DecreaseOrderItemCountCommand command)
    {
        var result = await _httpClient.PutAsJsonAsync("order/orderItem/DecreaseCount", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DeleteOrderItem(DeleteOrderItemCommand command)
    {
        var result = await _httpClient.DeleteAsync($"order/orderItem/{command.OrderItemId}" );
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<Order?> GetOrderById(long orderId)
    {
        var result = await _httpClient.GetFromJsonAsync<ApiResult<Order?>>($"order/{orderId}");
        return  result?.Data;

    }

    public async Task<OrderFilterResult> GetOrderFilter(OrderFilterParams filterParams)
    {
        var url = $"order/?pageId={filterParams.PageId}&take={filterParams.Take}";
        if (filterParams.StartDate != null)
            url += "&startDate=" + filterParams.StartDate;

        if (filterParams.EndDate != null)
            url += "&endDate=" + filterParams.EndDate;

        if (filterParams.Status != null)
            url += "&status=" + filterParams.Status;

        if (filterParams.UserId != null)
            url += "&UserId=" + filterParams.UserId;

        var result = await _httpClient.GetFromJsonAsync<ApiResult<OrderFilterResult>>(url);
        return result?.Data;
    }

    public async Task<Order?> GetCurrentOrder()
    {
        var result = await _httpClient.GetFromJsonAsync<ApiResult<Order?>>("order/current");
        return result?.Data;
    }

    public async Task<OrderFilterResult> GetUserOrders(int pageId, int take, OrderStatus? status)
    {
        var url = $"order/current/filter?pageId={pageId}&take={take}";
        if (status != null)
            url += $"&status={status}";

        var result = await _httpClient
            .GetFromJsonAsync<ApiResult<OrderFilterResult?>>(url);

        return result?.Data;
    }
}