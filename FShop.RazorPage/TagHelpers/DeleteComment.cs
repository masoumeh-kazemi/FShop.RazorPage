using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FShop.RazorPage.TagHelpers;

public class DeleteComment : TagHelper
{
    public string Url { get; set; }
    public string Description { get; set; } = "";
    public string Class { get; set; } = "btn btn-danger btn-sm";
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.Add("onClick", $"DeleteComment('{Url}','{Description}')");
        output.Attributes.Add("class", Class);
        base.Process(context, output);
    }


}