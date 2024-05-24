using FShop.RazorPage.Models;
using FShop.RazorPage.Models.ShippingMethods;

namespace FShop.RazorPage.Services.ShippingMethods;

public interface IShippingMethodService
{
    Task<List<ShippingMethodDto>> GetShippingMethods();
}

public class ShippingMethodService : IShippingMethodService
{
    private readonly HttpClient _httpClient;
    private const string ModuleName = "shippingMethod";
    public ShippingMethodService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ShippingMethodDto>> GetShippingMethods()
    {
        var result = await _httpClient.GetFromJsonAsync<ApiResult<List<ShippingMethodDto>>>($"{ModuleName}");
        return result?.Data?? new List<ShippingMethodDto>();
    }
}