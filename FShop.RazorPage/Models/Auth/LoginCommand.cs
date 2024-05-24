namespace FShop.RazorPage.Models.Auth;

public class LoginCommand
{
#pragma warning disable CS8618 // Non-nullable property 'PhoneNumber' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string PhoneNumber { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'PhoneNumber' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Password' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string Password { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Password' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
}