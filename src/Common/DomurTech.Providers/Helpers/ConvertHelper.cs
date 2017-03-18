using System;
using System.Linq;

namespace DomurTech.Providers.Helpers
{
    internal static class ConvertHelper
    {
        public static int ConvertToInt(this string str)
        {
            var numAmount = 0;
            if (!string.IsNullOrEmpty(str))
            {
                int.TryParse(str, out numAmount);
            }
            return numAmount;
        }

        public static bool ConvertToBoolean(this string str)
        {
            string[] trueStrings = { "1", "y", "yes", "true", "evet", "on" };
            string[] falseStrings = { "0", "n", "no", "false", "hayır", "hayir", "off" };
            if (trueStrings.Contains(str, StringComparer.OrdinalIgnoreCase)) return true;
            if (falseStrings.Contains(str, StringComparer.OrdinalIgnoreCase)) return false;
            throw new InvalidCastException("Yalnızca şu ifadeler dönüştürülür: " + string.Join(", ", trueStrings) + " ve " + string.Join(", ", falseStrings));
        }

    }
}
