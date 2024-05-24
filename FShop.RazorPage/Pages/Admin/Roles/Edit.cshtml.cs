using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Infrastructure.Utils;
using FShop.RazorPage.Models.Roles;
using FShop.RazorPage.Services.Roles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Admin.Roles
{
    [BindProperties]
    public class EditModel : BaseRazorPage
    {
        public string Title { get; set; }
        public List<Permission> Permissions { get; set; }
       
        private readonly IRoleService _roleService;

        public EditModel(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task OnGet(long id)
        {
            var role = await _roleService.GetRoleById(id);
            Permissions = role.Permissions;
            Title = role.Title;
        }

        //public async Task<IActionResult> OnPost(long id, string[] permission)
        //{
        //    var permissionModel = new List<Permission>();
        //    foreach (var item in permission)
        //    {
        //        permissionModel.Add(EnumUtils.ParseEnum<Permission>(item));
        //    }
        //    var result = await _roleService.EditRole(new EditRoleCommand()
        //    {
        //        Id = id,
        //        Permissions = permissionModel,
        //        Title = Title,
        //    });
        //    return RedirectAndShowAlert(result, RedirectToPage("Index"));
        //}

        public async Task<IActionResult> OnPost(long id, List<Permission> permissions)
        {
            var result = await _roleService.EditRole(new EditRoleCommand()
            {
                Title = Title,
                Permissions = permissions,
                Id = id
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"), RedirectToPage("Edit", new { id }));
        }
    }
}
