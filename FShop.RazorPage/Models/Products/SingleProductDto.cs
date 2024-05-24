using FShop.RazorPage.Models.Sellers;

namespace FShop.RazorPage.Models.Products;

public class SingleProductDto
{
    public ProductDto Product { get; set; }
    public List<InventoryDto> Inventrories { get; set; }
}