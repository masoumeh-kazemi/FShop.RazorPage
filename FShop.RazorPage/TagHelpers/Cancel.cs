using System.Diagnostics;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FShop.RazorPage.TagHelpers;


public class Cancel : TagHelper
{
    private readonly IHttpContextAccessor _accessor;

    public Cancel(IHttpContextAccessor contextAccessor)
    {
        _accessor = contextAccessor;
    }

    public string BackUrl { get; set; }
    public string Text { get; set; } = "انصراف";

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var backUrl = RefererUrl();

        output.TagName = "a";
        output.Attributes.Add("href", BackUrl ?? backUrl);
        output.Attributes.Add("class", "btn btn-danger glow");
        output.Content.SetContent(Text);
    }

    private string RefererUrl()
    {
        var backUrl = _accessor.HttpContext.Request.Headers["Referer"];
        if (string.IsNullOrWhiteSpace(backUrl))
            backUrl = "/";

        return backUrl;
    }
}

public class Submit : TagHelper
{
    public string Text { get; set; } = "ثبت اطلاعات";
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "input";
        output.Attributes.Add("class","btn btn-success shadow");
        output.Attributes.Add("type", "submit");
        output.Attributes.Add("value", Text);
    }


}