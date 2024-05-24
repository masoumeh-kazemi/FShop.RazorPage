namespace FShop.RazorPage.Infrastructure.Utils;

public class EnumUtils
{
    public static T ParseEnum<T>(string value)
    {
        var result = (T)Enum.Parse(typeof(T), value, true);
        return result;
    }
}