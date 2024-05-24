namespace FShop.RazorPage.Models.Products.Commands;

public class DeleteImageProductCommand
{
    public long ProductId { get; set; }
    public long ImageId { get; set; }
}