namespace FShop.RazorPage.Models.Products.Commands;

public class AddImageProductCommand
{
#pragma warning disable CS8618 // Non-nullable property 'ImageFile' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public IFormFile ImageFile { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'ImageFile' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public long ProductId { get; set; }
    public int Sequence { get; set; }
}