using FShop.RazorPage.Models;

namespace FShop.RazorPage.Services.Transactions;

public class CreateTransactionCommand
{
    public long OrderId { get; set; }
    public string SuccessCallBackUrl { get; set; }
    public string ErrorCallBackUrl { get; set; }
}
public interface ITransactionService
{
    Task<ApiResult<string>> CreateTransaction(CreateTransactionCommand command);
}

public class TransactionService : ITransactionService
{
    private readonly HttpClient _httpClient;
    private const string ModuleName = "transaction";
    public TransactionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<ApiResult<string>> CreateTransaction(CreateTransactionCommand command)
    {
       var result = await _httpClient.PostAsJsonAsync(ModuleName,command);
       return await result.Content.ReadFromJsonAsync<ApiResult<string>>();
    }
}