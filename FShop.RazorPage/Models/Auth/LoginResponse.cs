namespace FShop.RazorPage.Models.Auth;

public class LoginResponse
{
#pragma warning disable CS8618 // Non-nullable property 'Token' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string Token { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Token' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Refreshtoken' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string Refreshtoken { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Refreshtoken' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
}