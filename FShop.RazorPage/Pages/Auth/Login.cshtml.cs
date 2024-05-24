using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FShop.RazorPage.Infrastructure;
using FShop.RazorPage.Infrastructure.CookieUtiles;
using FShop.RazorPage.Models.Auth;
using FShop.RazorPage.Models.Orders.Command;
using FShop.RazorPage.Services.Auth;
using FShop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace FShop.RazorPage.Pages.Auth
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        #region Properties

        [DisplayName("شماره تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PhoneNumber { get; set; }

        [DisplayName(" رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "کلمه عبور باید بیشتر از 5 کارکتر باشد")]
        public string Password { get; set; }
        public string RedirectTo { get; set; }


        #endregion

        private readonly IAuthService _authService;
        private readonly ShopCartCookieManager _shopCartCookieManager;
        private readonly IOrderService _orderService;
        public LoginModel(IAuthService authService, ShopCartCookieManager shopCartCookieManager, IOrderService orderService)

        {
            _authService = authService;
            _shopCartCookieManager = shopCartCookieManager;
            _orderService = orderService;
        }
        public IActionResult OnGet(string redirectTo)
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/");

            RedirectTo = redirectTo;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _authService.Login(new LoginCommand()
            {
                Password = Password,
                PhoneNumber = PhoneNumber
            });
            if (result.IsSuccess == false)
            {
                ModelState.AddModelError(nameof(PhoneNumber), result.MetaData.Message);
                return Page();
            }

            var token = result.Data.Token;
            var refreshToken = result.Data.Refreshtoken;
            HttpContext.Response.Cookies.Append("token", token, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddDays(7)
            });
            HttpContext.Response.Cookies.Append("refresh-token", refreshToken, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddDays(10)
            });

            await SyncShopCart(token);
            if (string.IsNullOrWhiteSpace(RedirectTo) == false)
            {
                return LocalRedirect(RedirectTo);
            }
            return Redirect("/");
        }

        private async Task SyncShopCart(string token)
        {
            var shopCart = _shopCartCookieManager.GetShopCart();
            if (shopCart == null || shopCart.Items.Any() == false)
                return;

            HttpContext.Request.Headers.Append("Authorization", $"Bearer {token}");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            var userId = Convert.ToInt64(jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            foreach (var item in shopCart.Items)
            {
                await _orderService.AddOrderItem(new AddOrderItemCommand()
                {
                    Count = item.Count,
                    UserId = userId,
                    InventoryId = item.InventoryId
                });
            }
            _shopCartCookieManager.DeleteShopCart();
        }
    }












    //    public void OnGet()
    //    {

    //    }

    //    public async Task<IActionResult> OnPost()
    //    {
    //        var result = await _authService.Login(new LoginCommand()
    //        {
    //            Password = Password,
    //            PhoneNumber = PhoneNumber
    //        });

    //        if (result.IsSuccess == false)
    //        {
    //            ModelState.AddModelError(nameof(PhoneNumber), result.MetaData.Message);
    //            return Page();
    //        }

    //        var token = result.Data.Token;
    //        var refreshToken = result.Data.Refreshtoken;
    //        HttpContext.Response.Cookies.Append("token", token, new CookieOptions()
    //        {
    //            HttpOnly = true,
    //            Expires = DateTimeOffset.Now.AddDays(7)
    //        });

    //        HttpContext.Response.Cookies.Append("refresh-token", refreshToken, new CookieOptions()
    //        {
    //            HttpOnly = true,
    //            Expires = DateTimeOffset.Now.AddDays(8)

    //        });

    //        await SyncShopCart(token);
    //        var userId = User.GetUserId();
    //        return Redirect("/");
    //    }

    //    private async Task SyncShopCart(string token)
    //    {
    //        var shopCart = _shopCartCookieManager.GetShopCart();
    //        if (shopCart == null || shopCart.Items.Any() == false)
    //            return;

    //        HttpContext.Request.Headers.Append("Authorization", $"Bearer {token}");

    //        //decode jwt token
    //        //because can not read claim information from jwt token 
    //        var handler = new JwtSecurityTokenHandler();
    //        var jwtSecurityToken = handler.ReadJwtToken(token);
    //        var userId = Convert.ToInt64(jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
    //        foreach (var item in shopCart.Items)
    //        {

    //            await _orderService.AddOrderItem(new AddOrderItemCommand()
    //            {
    //                Count = item.Count,
    //                InventoryId = item.InventoryId,
    //                UserId = userId,
    //            });
    //        }
    //        _shopCartCookieManager.DeleteShopCart();

    //    }
    //}
}
