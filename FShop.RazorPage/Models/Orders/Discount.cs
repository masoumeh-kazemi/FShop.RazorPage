namespace FShop.RazorPage.Models.Orders;

public class Discount
{
#pragma warning disable CS8618 // Non-nullable property 'DiscountTitle' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string DiscountTitle { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'DiscountTitle' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public int DiscountAmount { get; set; }
}