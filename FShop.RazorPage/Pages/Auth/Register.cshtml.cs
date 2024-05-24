using FShop.RazorPage.Models.Auth;
using FShop.RazorPage.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using FShop.RazorPage.Infrastructure.RazorUtil;

namespace FShop.RazorPage.Pages.Auth
{
    [BindProperties]
    public class RegisterModel : BaseRazorPage
    {
        #region Propeties
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string PhoneNumber { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(5, ErrorMessage = "کلمه عبور باید بزرگتر ار 5 کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Compare("Password", ErrorMessage = "کلمه های عبورر یکسان نیستند")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        #endregion



        private readonly IAuthService _authService;

#pragma warning disable CS8618 // Non-nullable property 'PhoneNumber' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Password' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'ConfirmPassword' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public RegisterModel(IAuthService authService)
#pragma warning restore CS8618 // Non-nullable property 'ConfirmPassword' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning restore CS8618 // Non-nullable property 'Password' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning restore CS8618 // Non-nullable property 'PhoneNumber' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        {
            _authService = authService;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            var result = await _authService.Register(new RegisterCommand()
            {
                Password = Password,
                ConfirmPassword = Password,
                PhoneNumber = PhoneNumber,

            });

#pragma warning disable CS8604 // Possible null reference argument for parameter 'result' in 'IActionResult BaseRazorPage.RedirectAndShowAlert(ApiResult result, IActionResult redirectPath)'.
            return RedirectAndShowAlert(result, RedirectToPage("Login"));
#pragma warning restore CS8604 // Possible null reference argument for parameter 'result' in 'IActionResult BaseRazorPage.RedirectAndShowAlert(ApiResult result, IActionResult redirectPath)'.
        }
    }
}
