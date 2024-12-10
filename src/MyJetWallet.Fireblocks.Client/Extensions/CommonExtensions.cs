using System.Globalization;

namespace MyJetWallet.Fireblocks.Client.Extensions
{
    public static class CommonExtensions
    {
        public static decimal TryStringToConvert(this string value)
        {
            return !string.IsNullOrEmpty(value) && decimal.TryParse(value, out var result)
                ? result : 0m;
        }

        public static string TryConvertToString(this decimal value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}