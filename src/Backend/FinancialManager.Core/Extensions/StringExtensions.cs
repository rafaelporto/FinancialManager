using System.Linq;

namespace FinancialManager.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool NotContainsSpecialCaracters(this string value) =>
            !value.Any(s => !char.IsLetterOrDigit(s) && !char.IsWhiteSpace(s));
    }
}
