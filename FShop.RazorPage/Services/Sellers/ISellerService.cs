using FShop.RazorPage.Infrastructure;
using FShop.RazorPage.Models;
using FShop.RazorPage.Models.Sellers;
using FShop.RazorPage.Models.Sellers.Command;

namespace FShop.RazorPage.Services.Sellers;

public interface ISellerService
{
    Task<ApiResult> CreateSeller(CreateSellerCommand command);
    Task<ApiResult> EditSeller(EditSellerCommand command);
    Task<ApiResult> AddInventory(AddSellerInventoryCommand command);
    Task<ApiResult> EditInventory(EditSellerInventoryCommand command);

    Task<SellerDto?> GetSellerById(long sellerId);
    Task<SellerDto?> GetCurrentSeller();
    Task<SellerFilterResult> GetSellersByFilter(SellerFilterParams filterParams);

    Task<InventoryDto?> GetInventoryById(long inventoryId);
    Task<List<InventoryDto>> GetSellerInventories();

}

public class SellerService : ISellerService
{
    private readonly HttpClient _httpClient;
    private const string ModuleName = "seller";

    public SellerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<ApiResult> CreateSeller(CreateSellerCommand command)
    {
        var result = await _httpClient.PostAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditSeller(EditSellerCommand command)
    {
        var result = await _httpClient.PutAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> AddInventory(AddSellerInventoryCommand command)
    {
        var result = await _httpClient.PostAsJsonAsync($"{ModuleName}/inventory", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditInventory(EditSellerInventoryCommand command)
    {
        var result = await _httpClient.PutAsJsonAsync($"{ModuleName}/inventory", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<SellerDto?> GetSellerById(long sellerId)
    {
        var result = await _httpClient.GetFromJsonAsync<ApiResult<SellerDto?>>($"{ModuleName}/{sellerId}");
        return result?.Data;
    }

    public async Task<SellerDto?> GetCurrentSeller()
    {
        var result = await _httpClient.GetFromJsonAsync<ApiResult<SellerDto?>>($"{ModuleName}/current");
        return result?.Data;
    }

    public async Task<SellerFilterResult> GetSellersByFilter(SellerFilterParams filterParams)
    {
        var url = filterParams.GenerateBaseFilterUrl(ModuleName) + $"&&nationalCode={filterParams.NationalCode}" +
                  $"&&shopName={filterParams.ShopName}";

        var result = await _httpClient.GetFromJsonAsync<ApiResult<SellerFilterResult>>(url);
        return result?.Data;
    }

    public async Task<InventoryDto?> GetInventoryById(long inventoryId)
    {
        var result = await _httpClient.GetFromJsonAsync<ApiResult<InventoryDto?>>($"{ModuleName}/inventory/{inventoryId}");
        return result?.Data;
    }

    public async Task<List<InventoryDto>> GetSellerInventories()
    {
        var result = await _httpClient.GetFromJsonAsync<ApiResult<List<InventoryDto>>>($"{ModuleName}/inventory");
        return result?.Data;
    }
}