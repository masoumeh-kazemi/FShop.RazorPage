using AutoMapper;
using FShop.RazorPage.Models.UserAddress;
using FShop.RazorPage.ViewModels.Users.Addresses;

namespace FShop.RazorPage.Infrastructure;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateUserAddressCommand, CreateUserAddressViewModel>().ReverseMap();
        CreateMap<AddressDto, EditUserAddressViewModel>().ReverseMap();

    }
}