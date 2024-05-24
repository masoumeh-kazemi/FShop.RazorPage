namespace FShop.RazorPage.Models.Users;

public class UserTokenDto : BaseDto
{
    public long UserId { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'HashJwtToken' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string HashJwtToken { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'HashJwtToken' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'HashRefreshToken' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string HashRefreshToken { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'HashRefreshToken' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public DateTime TokenExpireDate { get; set; }
    public DateTime RefreshTokenExpireDate { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'Device' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string Device { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Device' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
}