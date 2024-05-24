using FShop.RazorPage.Infrastructure;
using FShop.RazorPage.Infrastructure.CookieUtiles;
using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Models;
using FShop.RazorPage.Models.Orders;
using FShop.RazorPage.Models.Orders.Command;
using FShop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages
{

    public class ShopCartModel : BaseRazorPage
    {
        private readonly IOrderService _orderService;
        private readonly ShopCartCookieManager _shopCartCookieManager;
        public ShopCartModel(IOrderService orderService, ShopCartCookieManager shopCartCookieManager)
        {
            _orderService = orderService;
            _shopCartCookieManager = shopCartCookieManager;
        }

        public Order? Order { get; set; }
        public async Task OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                Order = await _orderService.GetCurrentOrder();
            }
            else
            {
                Order = _shopCartCookieManager.GetShopCart();
            }
        }

        public async Task<IActionResult> OnPostDeleteItem(long id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await AjaxTryCatch(() => _orderService.DeleteOrderItem(new DeleteOrderItemCommand()
                {
                    OrderItemId = id
                }));
            }
            else
            {
                return await AjaxTryCatch(async () =>
                {
                    _shopCartCookieManager.DeleteOrderItem(id);
                    return ApiResult.Success();
                });
            }


        }

        public async Task<IActionResult> OnPostIncreaseItemCount(long id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await AjaxTryCatch(() => _orderService.IncreaseOrderItemCount(new IncreaseOrderItemCountCommand()
                {
                    Count = 1,
                    UserId = User.GetUserId(),
                    ItemId = id

                }));
            }
            else
            {
                return await AjaxTryCatch(async () =>
                {
                    _shopCartCookieManager.Increase(id);
                    return ApiResult.Success();
                });
            }
        }
        public async Task<IActionResult> OnPostDecreaseItemCount(long id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await AjaxTryCatch(() => _orderService.DecreaseOrderItemCount(new DecreaseOrderItemCountCommand()
                {
                    Count = 1,
                    UserId = User.GetUserId(),
                    ItemId = id
                }));
            }
            else
            {
                return await AjaxTryCatch(async () =>
                {
                    _shopCartCookieManager.Decrease(id);
                    return ApiResult.Success();
                });
            }
        }
        public async Task<IActionResult> OnPostAddItem(long inventoryId, int count)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await AjaxTryCatch(() => _orderService.AddOrderItem(new AddOrderItemCommand()
                {
                    UserId = User.GetUserId(),
                    InventoryId = inventoryId,
                    Count = count
                }));
            }
            else
            {
                return await AjaxTryCatch(() => _shopCartCookieManager.AddItem(inventoryId, count));
            }
        }

        public async Task<IActionResult> OnGetShopCartDetail()
        {
            Order? order = new();
            if (User.Identity.IsAuthenticated)
            {
                order = await _orderService.GetCurrentOrder();
            }
            else
            {
                order = _shopCartCookieManager.GetShopCart();
            }

            return new ObjectResult(new
            {
                items = order?.Items,
                count = order?.Items.Sum(s => s.Count),
                price = $"{order?.Items.Sum(s => s.TotalPrice):#,0} تومان"
            });
        }
    }

    //public class ShopCartModel : BaseRazorPage
    //{
    //    private readonly IOrderService _orderService;
    //    private readonly ShopCartCookieManager _shopCartCookieManager;

    //    public ShopCartModel(IOrderService orderService, ShopCartCookieManager shopCartCookieManager)
    //    {
    //        _orderService = orderService;
    //        _shopCartCookieManager = shopCartCookieManager;
    //    }

    //    public Order? Order { get; set; }
    //    public async Task OnGet()
    //    {
    //        if (User.Identity.IsAuthenticated)
    //        {
    //            Order = await _orderService.GetCurrentOrder();
    //        }
    //        else
    //        {
    //            Order = _shopCartCookieManager.GetShopCart();
    //        }
    //    }

    //    public async Task<IActionResult> OnPostDeleteItem(long id)
    //    {
    //        if (User.Identity.IsAuthenticated)
    //        {
    //            return await AjaxTryCatch(() =>
    //            {
    //                return _orderService.DeleteOrderItem(new DeleteOrderItemCommand()
    //                {
    //                    OrderItemId = id
    //                });
    //            });
    //        }
    //        else
    //        {
    //            return await AjaxTryCatch(async () =>
    //            {
    //                 _shopCartCookieManager.DeleteOrderItem(id);
    //                 return ApiResult.Success();
    //            });
    //            return Page();
    //        }


    //    }

    //    public async Task<IActionResult> OnPostIncreaseItemCount(long id)
    //    {
    //        if (User.Identity.IsAuthenticated)
    //        {
    //            return await AjaxTryCatch(() =>
    //            {
    //                return _orderService.IncreaseOrderItemCount(new IncreaseOrderItemCountCommand()
    //                {
    //                    UserId = User.GetUserId(),
    //                    ItemId = id,
    //                    Count = 1
    //                });
    //            });
    //        }

    //        else
    //        {
    //            return await AjaxTryCatch(async () =>
    //            {
    //                _shopCartCookieManager.Increase(id);
    //                return ApiResult.Success();
    //            });
    //        }

    //    }

    //    public async Task<IActionResult> OnPostDecreaseItemCount(long id)
    //    {
    //        if (User.Identity.IsAuthenticated)
    //        {
    //            return await AjaxTryCatch(() =>
    //            {
    //                return _orderService.DecreaseOrderItemCount(new DecreaseOrderItemCountCommand()
    //                {
    //                    Count = 1,
    //                    ItemId = id,
    //                    UserId = User.GetUserId()
    //                });
    //            });
    //        }
    //        else
    //        {
    //            return await AjaxTryCatch(async () =>
    //            {
    //                _shopCartCookieManager.Decrease(id);
    //                return ApiResult.Success();
    //            });
    //        }
    //    }

    //    public async Task<IActionResult> OnPostAddItem(long inventoryId, int count)
    //    {
    //        if (User.Identity.IsAuthenticated)
    //        {
    //            return await AjaxTryCatch(() =>
    //            {
    //                return _orderService.AddOrderItem(new AddOrderItemCommand()
    //                {
    //                    UserId = User.GetUserId(),
    //                    InventoryId = inventoryId,
    //                    Count = count
    //                });
    //            });
    //        }
    //        else
    //        {
    //            return await AjaxTryCatch(() =>
    //            {
    //                return _shopCartCookieManager.AddItem(inventoryId, count);
    //            });
    //        }

    //    }

    //    public async Task<IActionResult> OnGetShopCartDetail()
    //    {
    //        Order? order = new();
    //        if (User.Identity.IsAuthenticated)
    //        {
    //            order = await _orderService.GetCurrentOrder();
    //        }
    //        else
    //        {
    //            order = _shopCartCookieManager.GetShopCart();
    //        }

    //        return new ObjectResult(new
    //        {
    //            items = order?.Items,
    //            count = order?.Items.Sum(s => s.Count),
    //            price = $"{order?.Items.Sum(s => s.TotalPrice):#,0} تومان"
    //        });
    //    }

    //    //public async Task<IActionResult> OnGetShopCartDetail()
    //    //{
    //    //    Order? order = new Order();
    //    //    if (User.Identity.IsAuthenticated)
    //    //    {
    //    //         order = await _orderService.GetCurrentOrder();
    //    //    }
    //    //    else
    //    //    {
    //    //         order = _shopCartCookieManager.GetShopCart();
    //    //    }

    //    //    return new ObjectResult(new
    //    //        { items = order?.Items, 
    //    //            count = order?.Items.Sum(f=>f.Count),
    //    //        //price = $"{order?.Items.Sum(f => f.TotalPrice).ToString("#,0")} تومان"
    //    //        //or
    //    //        price = $"{order?.Items.Sum(f => f.TotalPrice):#,0} تومان"

    //    //    });
    //    //}
    //}
}
