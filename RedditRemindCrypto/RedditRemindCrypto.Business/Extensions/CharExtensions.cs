using System.Globalization;

namespace RedditRemindCrypto.Business.Extensions
{
    public static class CharExtensions
    {
        public static bool IsNumber(this char character)
        {
            return char.IsNumber(character);
        }

        public static bool IsSpace(this char character)
        {
            return char.IsWhiteSpace(character);
        }

        public static bool IsLetter(this char character)
        {
            return char.IsLetter(character);
        }

        public static bool IsCurrencySymbol(this char character)
        {
            return char.GetUnicodeCategory(character) == UnicodeCategory.CurrencySymbol;
        }
    }
}
