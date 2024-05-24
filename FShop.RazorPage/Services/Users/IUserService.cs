﻿using FShop.RazorPage.Models;
using System.Net;
using FShop.RazorPage.Infrastructure;
using FShop.RazorPage.Models.Users.Commands;
using FShop.RazorPage.Models.Users;
using System.Net.Http;

namespace FShop.RazorPage.Services.Users;

public interface IUserService
{
    Task<ApiResult> CreateUser(CreateUserCommand command);
    Task<ApiResult> EditUser(EditUserCommand command);
    Task<ApiResult> EditUserCurrent(EditUserCommand command);
    Task<ApiResult> ChangePassword(ChangePasswordCommand command);

    Task<UserDto?> GetUserById(long userId);
    Task<UserDto?> GetCurrentUser();
    Task<UserFilterResult> GetUsersByFilter(UserFilterParams filterParams);
}
public class UserService : IUserService
{
    private readonly HttpClient _client;
    private const string ModuleName = "user";
    public UserService(HttpClient client)
    {
        _client = client;
    }
    public async Task<ApiResult> CreateUser(CreateUserCommand command)
    {
        var result = await _client.PostAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditUser(EditUserCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.PhoneNumber), "PhoneNumber");
        formData.Add(new StreamContent(command.Avatar.OpenReadStream()), "Avatar");
        formData.Add(new StringContent(command.Gender.ToString()), "Position");
        formData.Add(new StringContent(command.Name), "Name");
        formData.Add(new StringContent(command.Family), "Family");
        formData.Add(new StringContent(command.Email), "Email");

        var result = await _client.PutAsync(ModuleName, formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }


    public async Task<ApiResult> EditUserCurrent(EditUserCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.PhoneNumber), "PhoneNumber");
        formData.Add(new StreamContent(command.Avatar.OpenReadStream()), "Avatar", command.Avatar.FileName);
        formData.Add(new StringContent(command.Gender.ToString()), "Position");
        formData.Add(new StringContent(command.Name), "Name");
        formData.Add(new StringContent(command.Family), "Family");
        formData.Add(new StringContent(command.Email), "Email");

        var result = await _client.PutAsync($"{ModuleName}/current", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }


    public async Task<ApiResult> ChangePassword(ChangePasswordCommand command)
    {
        var result = await _client.PutAsJsonAsync($"{ModuleName}/ChangePassword", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<UserDto?> GetUserById(long userId)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<UserDto?>>($"{ModuleName}/{userId}");
        return result?.Data;
    }

    public async Task<UserDto?> GetCurrentUser()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<UserDto?>>($"{ModuleName}/current");
        return result?.Data;
    }

    public async Task<UserFilterResult> GetUsersByFilter(UserFilterParams filterParams)
    {
        var url = filterParams.GenerateBaseFilterUrl(ModuleName) +
                  $"email={filterParams.Email}&phoneNumber={filterParams.PhoneNumber}&id={filterParams.Id}";

        var result = await _client.GetFromJsonAsync<ApiResult<UserFilterResult>>(url);
        return result.Data;
    }
}