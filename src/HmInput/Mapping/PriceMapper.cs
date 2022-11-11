using System.Text.RegularExpressions;

namespace HmInput.Mapping;

internal static class PriceMapper
{
    public static double? MapToDouble(string price)
    {
        if(price is null)
        {
            return null;
        }

        price = price.Replace(",", ".");
        var match = Regex.Match(price, "[0-9]+.[0-9]{2}");

        if(match is not null && double.TryParse(match.Value, out var number))
        {
            return number;
        }
        return null;
    }
}
