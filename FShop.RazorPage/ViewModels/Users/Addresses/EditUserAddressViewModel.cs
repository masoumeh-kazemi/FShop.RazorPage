using System.ComponentModel.DataAnnotations;

namespace FShop.RazorPage.ViewModels.Users.Addresses;

public class EditUserAddressViewModel
{
    public long Id { get; set; }
    [Display(Name = "استان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
#pragma warning disable CS8618 // Non-nullable property 'Shire' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string Shire { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Shire' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

    [Display(Name = "شهر")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
#pragma warning disable CS8618 // Non-nullable property 'City' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string City { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'City' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

    [Display(Name = "کدپستی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
#pragma warning disable CS8618 // Non-nullable property 'PostalCode' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string PostalCode { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'PostalCode' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

    [Display(Name = "آدرس پستی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
#pragma warning disable CS8618 // Non-nullable property 'PostalAddress' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string PostalAddress { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'PostalAddress' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

    [Display(Name = "شماره موبایل")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "شماره موبایل نامعتبر است")]
    [MinLength(11, ErrorMessage = "شماره موبایل نامعتبر است")]
#pragma warning disable CS8618 // Non-nullable property 'PhoneNumber' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string PhoneNumber { get; set; }

    [Display(Name = "نام")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Name { get; set; }

    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Family { get; set; }

    [Display(Name = "کدملی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MaxLength(10, ErrorMessage = "کدملی نامعتبر است")]
    [MinLength(10, ErrorMessage = "کدملی نامعتبر است")]
    public string NationalCode { get; set; }
}