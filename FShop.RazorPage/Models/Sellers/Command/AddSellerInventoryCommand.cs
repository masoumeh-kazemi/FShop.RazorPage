namespace FShop.RazorPage.Models.Sellers.Command;

public class AddSellerInventoryCommand
{
    public long SellerId { get; set; }
    public long ProductId { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int DiscountPercentage { get; set; }
}