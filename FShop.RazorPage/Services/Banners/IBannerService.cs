using FShop.RazorPage.Models;
using FShop.RazorPage.Models.Banners;

namespace FShop.RazorPage.Services.Banners;

public interface IBannerService
{
    Task<ApiResult> CreateBanner(CreateBannerCommand command);
    Task<ApiResult> EditBanner(EditBannerCommand command);
    Task<ApiResult> DeleteBanner(long bannerId);
    Task<BannerDto?> GetBannerById(long bannerId);

    Task<List<BannerDto>?> GetList();
}

public class BannerService : IBannerService
{
    private HttpClient _httpClient;

    public BannerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<ApiResult> CreateBanner(CreateBannerCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Link),"link");
        formData.Add(new StreamContent(command.ImageFile.OpenReadStream()),"ImageFile", command.ImageFile.FileName);
        formData.Add(new StringContent(command.Position.ToString()), "Position");

        var result = await _httpClient.PostAsync("banner", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditBanner(EditBannerCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Link), "link");
        formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);
        formData.Add(new StringContent(command.Position.ToString()), "Position");
        formData.Add(new StringContent(command.Id.ToString()), "Id");


        var result = await _httpClient.PutAsync("banner", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DeleteBanner(long bannerId)
    {
        var result = await _httpClient.DeleteAsync($"banner/{bannerId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<BannerDto?> GetBannerById(long bannerId)
    {
        var result = await _httpClient.GetFromJsonAsync<ApiResult<BannerDto>>($"banner/{bannerId}");
        return result?.Data;

    }

    public async Task<List<BannerDto>?> GetList()
    {
        var result = await _httpClient.GetFromJsonAsync<ApiResult<List<BannerDto>>>($"banner");

        if (result?.Data == null)
            return new List<BannerDto>();

        return result?.Data;
    }


}