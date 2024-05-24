using System.Xml.Schema;
using FShop.RazorPage.Models;
using FShop.RazorPage.Models.UserAddress;

namespace FShop.RazorPage.Services.UserAddress;

public interface IUserAddressService
{
    Task<ApiResult> CreateUserAddress(CreateUserAddressCommand command);
    Task<ApiResult> EditUserAddress(EditUserAddressCommand command);
    Task<ApiResult> DeleteUserAddress(long addressId);
    Task<ApiResult> SetActiveAddress(long addressId);

    Task<AddressDto?> GetUserAddressById(long id);
    Task<List<AddressDto>> GetUserAddresses();

}

public class UserAddressService  : IUserAddressService
{
    private readonly HttpClient _client;
    private const string ModuleName = "userAddress";

    public UserAddressService(HttpClient client)
    {
        _client = client;
    }
    public async Task<ApiResult> CreateUserAddress(CreateUserAddressCommand command)
    {
        var result = await _client.PostAsJsonAsync(ModuleName, command);
        var content = await result.Content.ReadFromJsonAsync<ApiResult>();
        return content;
    }

    public async Task<ApiResult> EditUserAddress(EditUserAddressCommand command)
    {
        var result = await _client.PutAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DeleteUserAddress(long addressId)
    {
        var result = await _client.DeleteAsync($"{ModuleName}/{addressId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> SetActiveAddress(long addressId)
    {
        var result = await _client.PutAsync($"{ModuleName}/SetActiveAddress/{addressId}",null);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<AddressDto?> GetUserAddressById(long id)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<AddressDto?>>($"{ModuleName}/{id}");
        return result?.Data;
    }

    public async Task<List<AddressDto>> GetUserAddresses()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<List<AddressDto>>>($"{ModuleName}");
        return result?.Data;
    }
}