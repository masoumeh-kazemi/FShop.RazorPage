using FShop.RazorPage.Models.Categories;
using FShop.RazorPage.Models.Comments;

namespace FShop.RazorPage.Models.Products;

public class ProductShopResult : BaseFilter<ProductShopDto, ProductShopFilterParam>
{
    public CategoryDto? CategoryDto { get; set; }
}

public class BaseFilter2<TData, TParam>
where TData : BaseDto
where TParam : BaseFilter
{
    public TData Data { get; set; }
    public TParam Param { get; set; }
}

public class ProductData :  BaseDto
{

}

public class ProductFilterParam2  : BaseFilterParam
{

}
public class ProductShopDto : BaseDto
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public long InventoryId { get; set; }
    public int Price { get; set; }
    public int DiscountPercentage { get; set; }
    public string ImageName { get; set; }

    public int TotalPrice { get; set; }
}
public class ProductShopFilterParam : BaseFilterParam
{
    public string? CategorySlug { get; set; } = "";
    public string? Search { get; set; } = "";
    public bool OnlyAvailableProducts { get; set; } = false;
    public bool? JustHasDiscount { get; set; } = false;
    public ProductSearchOrderBy SearchOrderBy { get; set; } = ProductSearchOrderBy.Cheapest;
}

public enum ProductSearchOrderBy
{
    Latest,
    Expensive,
    Cheapest,
}