using FShop.RazorPage.Infrastructure;
using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Models.Orders;
using FShop.RazorPage.Models.Orders.Command;
using FShop.RazorPage.Models.ShippingMethods;
using FShop.RazorPage.Models.UserAddress;
using FShop.RazorPage.Services.Orders;
using FShop.RazorPage.Services.ShippingMethods;
using FShop.RazorPage.Services.Transactions;
using FShop.RazorPage.Services.UserAddress;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Checkout
{
    public class IndexModel : BaseRazorPage
    {
        private readonly IOrderService _orderService;
        private readonly IUserAddressService _userAddressService;
        private readonly IShippingMethodService _shippingMethodService;
        private readonly ITransactionService _transactionService;
        public IndexModel(IOrderService orderService, IUserAddressService userAddressService, IShippingMethodService shippingMethodService, ITransactionService transactionService)
        {
            _orderService = orderService;
            _userAddressService = userAddressService;
            _shippingMethodService = shippingMethodService;
            _transactionService = transactionService;
        }

        public Order Order { get; set; }
        public List<AddressDto> Addresses { get; set; }
        public List<ShippingMethodDto> ShippingMethods { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var order = await _orderService.GetCurrentOrder();
            if (order==null)
                return RedirectToPage("../Index");

            Order = order;

            Addresses = await _userAddressService.GetUserAddresses();
            ShippingMethods  = await _shippingMethodService.GetShippingMethods();
            if (ShippingMethods.Any() == false)
                return RedirectToPage("../Index");

            return Page();
        }

        public async Task<IActionResult> OnPost(long shippingMethodId, long orderId)
        {
            var addresses = await _userAddressService.GetUserAddresses();
            
            var currentAddress = addresses.FirstOrDefault(f => f.ActiveAddress);
            if (currentAddress == null)
            {
                return Redirect("Index");
            }

            var result = await _orderService.CheckoutOrderItem(new CheckoutOrderCommand
            {
                UserId = User.GetUserId(),
                Shire = currentAddress.Shire,
                City = currentAddress.City,
                PostalCode = currentAddress.PostalCode,
                PostalAddress = currentAddress.PostalAddress,
                PhoneNumber = currentAddress.PhoneNumber,
                Name = currentAddress.Name,
                Family = currentAddress.Family,
                NationalCode = currentAddress.NationalCode,
                ShippingMethodId = shippingMethodId,
                

            });
            
            if (result.IsSuccess)
            {
                var userId = User.GetUserId();
                //var currentOrder = await _orderService.GetCurrentOrder();
                var res = await _transactionService.CreateTransaction(new CreateTransactionCommand()
                {
                    ErrorCallBackUrl =
                        $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Checkout/FinallyOrder/{orderId}",
                    SuccessCallBackUrl =
                        $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Checkout/FinallyOrder/{orderId}",
                    OrderId = orderId
                });
                if (res.IsSuccess)
                {
                    return Redirect(res.Data);
                }
            }

            ErrorAlert(result.MetaData.Message);
            return RedirectToPage("Index");
        }
    }
}
