using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Models.Roles;
using FShop.RazorPage.Services.Roles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using FShop.RazorPage.Infrastructure.Utils;

namespace FShop.RazorPage.Pages.Admin.Roles
{
    [BindProperties]
    public class AddModel : BaseRazorPage
    {
        private readonly IRoleService _roleService;

        public AddModel(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [BindProperty]
        public string Title { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string[] permission)
        {
            var permissionModel = new List<Permission>();
            foreach (var item in permission)
            {
                try
                {
                    permissionModel.Add(EnumUtils.ParseEnum<Permission>(item));
                }
                catch
                {
                    // 
                }
            }
            var result = await _roleService.CreateRole(new CreateRoleCommand()
            {
                Title = Title,
                Permissions = permissionModel
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));

        }
    }
}
