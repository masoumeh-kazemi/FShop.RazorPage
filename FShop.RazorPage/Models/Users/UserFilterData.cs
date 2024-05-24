using FShop.RazorPage.Models.Comments;

namespace FShop.RazorPage.Models.Users;

public class UserFilterData : BaseDto
{
#pragma warning disable CS8618 // Non-nullable property 'Name' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Name' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Family' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string Family { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Family' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'PhoneNumber' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string PhoneNumber { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'PhoneNumber' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string? Email { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'AvatarName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public string AvatarName { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'AvatarName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public Gender Gender { get; set; }
}

public class UserFilterParams : BaseFilterParam
{
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public long? Id { get; set; }
}

public class UserFilterResult : BaseFilter<UserFilterData, UserFilterParams>
{

}