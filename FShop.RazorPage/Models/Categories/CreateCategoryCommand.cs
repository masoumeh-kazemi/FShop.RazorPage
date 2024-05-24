namespace FShop.RazorPage.Models.Categories;

public class CreateCategoryCommand
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
}


public class EditCategoryCommand
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
}
public class AddChildCategoryCommand
{
    public long ParentId { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
}