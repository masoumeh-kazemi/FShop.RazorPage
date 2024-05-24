namespace FShop.RazorPage.Models.Banners;

public class EditBannerCommand
{
    public long Id { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'Link' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string Link { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Link' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'ImageFile' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public IFormFile ImageFile { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'ImageFile' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public BannerPosition Position { get; set; }
}