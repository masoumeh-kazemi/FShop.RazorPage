using AutoMapper;
using FShop.RazorPage.Infrastructure.RazorUtil;
using FShop.RazorPage.Models;
using FShop.RazorPage.Models.UserAddress;
using FShop.RazorPage.Services.UserAddress;
using FShop.RazorPage.ViewModels.Users.Addresses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FShop.RazorPage.Pages.Profile.Addresses
{
    [BindProperties]
    public class IndexModel : BaseRazorPage
    {

        #region Properties

        public List<AddressDto> Addresses { get; set; }
        

        #endregion
        private readonly IUserAddressService _userAddressService;
        private readonly IRenderViewToString _renderViewToString;
        private readonly IMapper _mapper;
        public IndexModel(IUserAddressService userAddressService, IRenderViewToString renderViewToString, IMapper mapper)
        {
            _userAddressService = userAddressService;
            _renderViewToString = renderViewToString;
            _mapper = mapper;
        }

        public async Task OnGet()
        {
            Addresses = await _userAddressService.GetUserAddresses();
        }

        public async Task<IActionResult> OnPostAsync(long addressId)
        {
            var result = await _userAddressService.DeleteUserAddress(addressId);
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
        public async Task<IActionResult> OnPostAddAddress(CreateUserAddressViewModel viewModel)
        {
            var model = _mapper.Map<CreateUserAddressCommand>(viewModel);
            return await AjaxTryCatch(async () =>
            {
                var result = await _userAddressService.CreateUserAddress(model);
                return result;
            },true);
        }
        public async Task<IActionResult> OnGetSetActiveAddress(long addressId)
        {
            return await AjaxTryCatch(async () =>
            {
                var result = await _userAddressService.SetActiveAddress(addressId);
                return result;
            }, true);
        }


        public async Task<IActionResult> OnPostEditAddress(EditUserAddressViewModel viewModel)
        {
            var model = new EditUserAddressCommand()
            {
                City = viewModel.City,
                Family = viewModel.Family,
                Name = viewModel.Name,
                Id = viewModel.Id,
                NationalCode = viewModel.NationalCode,
                PhoneNumber = viewModel.PhoneNumber,
                PostalCode = viewModel.PostalCode,
                PostalAddress = viewModel.PostalAddress,
                Shire = viewModel.Shire
            };
            //var model = _mapper.Map<EditUserAddressCommand>(viewModel);
            return await AjaxTryCatch(async () =>
            {
                var result = await _userAddressService.EditUserAddress(model);
                return result;
            }, true);
        }
        public async Task<IActionResult> OnGetShowAddPage()
        {
            var view = await _renderViewToString.RenderToStringAsync("_Add"
                , new CreateUserAddressViewModel(), PageContext);

            return await AjaxTryCatch(async () =>
            {
                return ApiResult<string>.Success(view);
            });
        }

        public async Task<IActionResult> OnGetShowEditPage(long addressId)
        {
            return await AjaxTryCatch(async () =>
            {
                var address = await _userAddressService.GetUserAddressById(addressId);
                var model = _mapper.Map<EditUserAddressViewModel>(address);
                var view = await _renderViewToString.RenderToStringAsync("_Edit", model,
                    PageContext);
                return ApiResult<string>.Success(view);
            });

        }
    }
}
