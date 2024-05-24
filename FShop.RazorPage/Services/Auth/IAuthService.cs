using System.Net;
using FShop.RazorPage.Models;
using FShop.RazorPage.Models.Auth;

namespace FShop.RazorPage.Services.Auth;

public interface IAuthService
{
    Task<ApiResult<LoginResponse>?> Login(LoginCommand command);
    Task<ApiResult?> Register(RegisterCommand command);
    Task<ApiResult<LoginResponse>?> RefreshToken();
    Task<ApiResult?> LogOut();

}

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _accessor;
    public AuthService(HttpClient httpClient, IHttpContextAccessor accessor)
    {
        _httpClient = httpClient;
        _accessor = accessor;
    }

    public async Task<ApiResult<LoginResponse>?> Login(LoginCommand command)
    {
        var result = await _httpClient.PostAsJsonAsync("auth/login", command);
        if (result.StatusCode != HttpStatusCode.OK)
            return new ApiResult<LoginResponse>() { IsSuccess = false };
        var loginUser = await result.Content.ReadFromJsonAsync<ApiResult<LoginResponse>>();
        return  loginUser;
    }

    //public async Task<ApiResult<LoginResponse>?> Login(LoginCommand command)
    //{
    //    var result = await _httpClient.PostAsJsonAsync("Auth/Login", command);

    //    return await result.Content.ReadFromJsonAsync<ApiResult<LoginResponse>>();
    //}


    public async Task<ApiResult?> Register(RegisterCommand command)
    {
        var result = await _httpClient.PostAsJsonAsync("auth/register", command);
        //if (result.StatusCode != HttpStatusCode.OK)
        //    return new ApiResult() { IsSuccess = false };
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult<LoginResponse>?> RefreshToken()
    {
        var refreshToken = _accessor.HttpContext.Request.Cookies["refreshToken"];
        var result = await _httpClient.PostAsync($"auth/RefreshToken?refreshToken={refreshToken}", null);
        return await result.Content.ReadFromJsonAsync<ApiResult<LoginResponse>>();
    }

    public async Task<ApiResult?> LogOut()
    {
        var result = await _httpClient.DeleteAsync("auth/Delete");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }
}