using FShop.RazorPage.Models;
using FShop.RazorPage.Models.Roles;

namespace FShop.RazorPage.Services.Roles;

public interface IRoleService
{
    Task<ApiResult> CreateRole(CreateRoleCommand command);
    Task<ApiResult> EditRole(EditRoleCommand command);

    Task<RoleDto?> GetRoleById(long roleId);
    Task<List<RoleDto>> GetRoless();
}

public class RoleService : IRoleService
{
    private readonly HttpClient _httpClient;
    private const string ModuleName = "role";

    public RoleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<ApiResult> CreateRole(CreateRoleCommand command)
    {
        var result = await _httpClient.PostAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditRole(EditRoleCommand command)
    {
        var result = await _httpClient.PutAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();

    }

    public async Task<RoleDto?> GetRoleById(long id)
    {
        var result = await _httpClient.GetFromJsonAsync<ApiResult<RoleDto?>>($"{ModuleName}/{id}");
        return result?.Data;
    }

    //public async Task<List<RoleDto>> GetRoles()
    //{
    //    var result = await _httpClient.GetFromJsonAsync<ApiResult<List<RoleDto>>>(ModuleName);
    //    return result.Data;
    //}

    public async Task<List<RoleDto>> GetRoless()
    {
        var result = await _httpClient.GetFromJsonAsync<ApiResult<List<RoleDto>>>(ModuleName);
        return result.Data;
    }
}