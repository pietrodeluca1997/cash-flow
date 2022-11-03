using System.Text.RegularExpressions;

namespace CF.Core.Helpers
{
    public static class BasicValidationHelper
    {
        private readonly static Regex OnlyLetters = new(@"^[a-zA-Z]+$");

        public static bool IsNullOrEmptyOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);
        public static bool IsNullOrNeutral(this int? value) => value is null || value == 0;
        public static bool IsNullOrNeutral(this decimal? value) => value is null || value == 0;
        public static bool IsNullOrNeutral(this float? value) => value is null || value == 0;
        public static bool IsNullOrNeutral(this double? value) => value is null || value == 0;

        public static bool IsStringValid(string value, int minLength, int maxLength, string propertyName, out IList<string> errors, Regex? pattern = null, bool onlyLetters = false)
        {
            errors = new List<string>();

            if (value.IsNullOrEmptyOrWhiteSpace())
            {
                errors.Add($"{propertyName} should be informed and cannot be null or empty");
            }

            if (value.Length < minLength || value.Length > maxLength)
            {
                errors.Add($"{propertyName} should have between {minLength} and {maxLength} characters");
            }

            if (onlyLetters)
            {
                if (!OnlyLetters.IsMatch(value))
                {
                    errors.Add($"{propertyName} should have only letters");
                }
            }

            if (pattern is not null)
            {
                if (!pattern.IsMatch(value))
                {
                    errors.Add($"{propertyName} should be valid");
                }
            }

            return !errors.Any();
        }
    }
}
