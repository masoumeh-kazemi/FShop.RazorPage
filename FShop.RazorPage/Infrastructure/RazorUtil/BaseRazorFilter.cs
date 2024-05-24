using FShop.RazorPage.Models.Comments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Infrastructure.RazorUtil;

public class BaseRazorFilter<TFilterParam> : PageModel where TFilterParam : BaseFilterParam, new()
{
    [BindProperty(SupportsGet = true)]
    public TFilterParam FilterParams { get; set; }

    public BaseRazorFilter()
    {
        FilterParams = new TFilterParam();
    }
}