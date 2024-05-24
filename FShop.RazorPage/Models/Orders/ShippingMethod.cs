namespace FShop.RazorPage.Models.Orders;

public class ShippingMethod
{
#pragma warning disable CS8618 // Non-nullable property 'ShippingType' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string ShippingType { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'ShippingType' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public int ShippingCost { get; set; }
}