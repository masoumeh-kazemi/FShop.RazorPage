using FShop.RazorPage.Models.Comments;

namespace FShop.RazorPage.Infrastructure;

public static class UrlGenerator
{
    public static string GenerateBaseFilterUrl(this BaseFilterParam filterParam, string moduleName)
    {
        var url = $"{moduleName}?pageId={filterParam.PageId}&&take={filterParam.Take}";
        return url;
    }
}