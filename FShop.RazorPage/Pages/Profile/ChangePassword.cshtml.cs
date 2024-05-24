using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Models.Users.Commands;
using FShop.RazorPage.Services.Users;


namespace FShop.RazorPage.Pages.Profile
{
    [BindProperties]
    public class ChangePasswordModel : BaseRazorPage
    {
        #region Properties

        [DisplayName("کلمه عبور فعلی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DisplayName("کلمه عبور جدید")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 5 کارکتر باشد")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [DisplayName("تکرار کلمه عبور جدید")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Compare("Password", ErrorMessage = "کلمه های عبور یکسان نیستند")]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }


        #endregion


        private readonly IUserService _userService;


        public ChangePasswordModel(IUserService userService)

        {
            _userService = userService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _userService.ChangePassword(new ChangePasswordCommand()
            {
                CurrentPassword = CurrentPassword,
                Password = Password,
                ConfirmPassword = ConfirmPassword,
                
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }

    }
}
