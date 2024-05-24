namespace FShop.RazorPage.Models.Sellers.Command;

public class CreateSellerCommand
{
    public long UserId { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'ShopName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string ShopName { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'ShopName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'NationalCode' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string NationalCode { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'NationalCode' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

}