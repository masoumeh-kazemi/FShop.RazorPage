using FShop.RazorPage.Models;
using FShop.RazorPage.Models.Categories;

namespace FShop.RazorPage.Services.Categories;

public interface ICategoryService
{
    Task<ApiResult> CreateCategory(CreateCategoryCommand command);
    Task<ApiResult> EditCategory(EditCategoryCommand command);
    Task<ApiResult> DeleteCategory(long categoryId);
    Task<ApiResult> AddChildCategory(AddChildCategoryCommand command);

    Task<CategoryDto> GetCategoryById(long categoryId);
    Task<List<CategoryDto>> GetCategories();
    Task<List<ChildCategoryDto>> GetChild(long parentCategoryId);

}

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<ApiResult> CreateCategory(CreateCategoryCommand command)
    {
        var result = await _httpClient.PostAsJsonAsync("category", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditCategory(EditCategoryCommand command)
    {
        var result = await _httpClient.PutAsJsonAsync("category", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DeleteCategory(long categoryId)
    {
        var result = await _httpClient.DeleteAsync($"category/{categoryId}" );
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> AddChildCategory(AddChildCategoryCommand command)
    {
        var result = await _httpClient.PostAsJsonAsync("category/addChild", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<CategoryDto?> GetCategoryById(long categoryId)
    {
        var result = await _httpClient.GetFromJsonAsync<ApiResult<CategoryDto>>($"category/{categoryId}");
        return result?.Data;
    }

    public async Task<List<CategoryDto>> GetCategories()
    {
        var result = await _httpClient.GetFromJsonAsync<ApiResult<List<CategoryDto>>>("category");
        return result?.Data;
    }

    public async Task<List<ChildCategoryDto>> GetChild(long parentCategoryId)
    {
        var result = await _httpClient.GetFromJsonAsync<ApiResult<List<ChildCategoryDto>>>($"category/getChild/{parentCategoryId}");
        return result?.Data;
    }
}