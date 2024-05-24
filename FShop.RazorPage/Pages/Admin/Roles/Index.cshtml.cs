using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Models.Roles;
using FShop.RazorPage.Services.Roles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Admin.Roles
{
    public class IndexModel : BaseRazorPage
    {
        private IRoleService _roleService;

        public IndexModel(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public List<RoleDto> Roles { get; set; }
        public async Task OnGet()
        {
            Roles = await _roleService.GetRoless();
        }
    }
}
