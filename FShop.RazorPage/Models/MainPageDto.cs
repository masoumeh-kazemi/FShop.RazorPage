using FShop.RazorPage.Models.Banners;
using FShop.RazorPage.Models.Products;
using FShop.RazorPage.Models.Sliders;

namespace FShop.RazorPage.Models;

public class MainPageDto
{
    public List<SliderDto> Sliders { get; set; }
    public List<BannerDto> Banners { get; set; }
    public List<ProductShopDto> SpectialProducts { get; set; }
    public List<ProductShopDto> LatestProducts { get; set; }
    public List<ProductShopDto> TopVisitProducts { get; set; }

}