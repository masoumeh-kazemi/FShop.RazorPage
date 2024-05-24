using FShop.RazorPage.Models;
using FShop.RazorPage.Models.Comments;
using System.ComponentModel.Design;
using FShop.RazorPage.Infrastructure;

namespace FShop.RazorPage.Services.Comments;

public interface ICommentService
{


    Task<ApiResult> AddComment(AddCommentCommand command);
    Task<ApiResult> EditComment(EditCommentCommand command);
    Task<ApiResult> ChangeStatus(long commentId, CommentStatus status);
    Task<ApiResult> DeleteComment(long commentId);



    Task<CommentFilterResult> GetCommentByFilter(CommentFilterParam filterParam);
    Task<CommentFilterResult> GetProductComments(int pageId, int take, long productId);

    Task<CommentDto?> GetCommentById(long id);
}

public class CommentService : ICommentService
{
    private readonly HttpClient _httpClient;

    public CommentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }



    public async Task<ApiResult> AddComment(AddCommentCommand command)
    {
        var result = await _httpClient.PostAsJsonAsync("comment", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditComment(EditCommentCommand command)
    {
        var result = await _httpClient.PutAsJsonAsync("comment", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> ChangeStatus(long commentId, CommentStatus status)
    {
        var result = await _httpClient.PostAsJsonAsync("comment/changeStatus", new {id = commentId, status = status});
        return await result.Content.ReadFromJsonAsync<ApiResult>();

    }

    public async Task<ApiResult> DeleteComment(long commentId)
    {
        var result = await _httpClient.DeleteAsync($"comment/{commentId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<CommentFilterResult> GetCommentByFilter(CommentFilterParam filterParams)
    {
        var url = filterParams.GenerateBaseFilterUrl("comment");
        if (filterParams.UserId != null)
            url += $"&UserId={filterParams.UserId}";

        if (filterParams.CommentStatus != null)
            url += $"&CommentStatus={filterParams.CommentStatus}";

        if (filterParams.StartDate != null)
            url += $"&StartDate{filterParams.StartDate}";

        if (filterParams.EndDate != null)
            url += $"&EndDate{filterParams.EndDate}";

        var result = await _httpClient.GetFromJsonAsync<ApiResult<CommentFilterResult>>(url);
        return result?.Data;
    }

    public async Task<CommentFilterResult> GetProductComments(int pageId, int take, long productId)
    {
        var url = $"comment/productComments?pageId={pageId}&take={take}&productId={productId}";
        var result = await _httpClient.GetFromJsonAsync<ApiResult<CommentFilterResult>>(url);
        return result?.Data;
    }
    public async Task<CommentDto?> GetCommentById(long id)
    {
        var result = await _httpClient.GetFromJsonAsync<CommentDto>($"Comment/{id}");
        return result;
    }
}