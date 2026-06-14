using System.Globalization;

namespace FirmPulse.Helpers;

public static class CurrencyHelper
{
    private static readonly CultureInfo Culture = new("en-IN");

    public static string ToInr(decimal amount) => string.Format(Culture, "{0:C}", amount);
}
