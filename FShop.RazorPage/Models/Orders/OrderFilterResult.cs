using FShop.RazorPage.Models.Comments;

namespace FShop.RazorPage.Models.Orders;

public class OrderFilterResult :  BaseFilter<OrderFilterData, OrderFilterParams>
{
    
}


public class OrderFilterData : BaseDto
{
    public long UserId { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'UserFullName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string UserFullName { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'UserFullName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public OrderStatus Status { get; set; }
    public string? Shire { get; set; }
    public string? City { get; set; }
    public ShippingMethod? ShippingMethod { get; set; }
    public int TotalPrice { get; set; }
    public int TotalItemCount { get; set; }
}

public class OrderFilterParams :  BaseFilterParam
{
    public long? UserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public OrderStatus? Status { get; set; }
}