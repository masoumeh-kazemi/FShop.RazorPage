using FShop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using FShop.RazorPage.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Models.Users.Commands;
using FShop.RazorPage.Services.Users;
using Microsoft.AspNetCore.Authorization;

namespace FShop.RazorPage.Pages.Profile
{
    [BindProperties] 
    public class EditModel : BaseRazorPage
    {

        private readonly IUserService _userService;


        public EditModel(IUserService userService)


        {
            _userService = userService;
        }
        #region Properties

        [Display(Name = "عکس پروفایل")]
        [FileImage(ErrorMessage = "تصویر پروفایل نامعتبر است")]
        public IFormFile? Avatar { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Family { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "شماره تلفن نامعتبر است")]
        [MinLength(11, ErrorMessage = "شماره تلفن معتبر است")]
        public string PhoneNumber { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Email { get; set; }

        public Gender Gender { get; set; } = Gender.None;

        #endregion


        public async Task OnGet()
        {
            var user = await _userService.GetCurrentUser();
            Name = user.Name;
            Family = user.Family;
            Email = user.Email;
            Gender = user.Gender;
            PhoneNumber = user.PhoneNumber;
            return;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _userService.EditUserCurrent(new EditUserCommand()
            {
                Name = Name,
                Family = Family,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Gender = Gender,
                Avatar = Avatar
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
