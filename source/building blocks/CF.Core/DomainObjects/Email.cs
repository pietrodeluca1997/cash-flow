using System.Text.RegularExpressions;

namespace CF.Core.DomainObjects
{
    public class Email
    {
        private static readonly Regex RegexEmail = new(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

        public const int MaxLength = 254;
        public const int MinLength = 3;
        public string Address { get; private set; }

        public Email(string address)
        {
            Address = string.Empty;

            if (!IsValid(address)) return;

            Address = address;
        }

        public static bool IsValid(string address, out IList<string> validationErrors)
        {
            validationErrors = new List<string>();

            if (string.IsNullOrWhiteSpace(address))
            {
                validationErrors.Add("E-mail should be informed and cannot be null or empty");
            }

            if (!(RegexEmail.IsMatch(address) && address.Length <= MaxLength && address.Length >= MinLength))
            {
                validationErrors.Add($"E-mail should be valid and have between {MinLength} and {MaxLength} characters");
            }

            return !validationErrors.Any();
        }

        public static bool IsValid(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return false;
            }

            if (!(RegexEmail.IsMatch(address) && address.Length <= MaxLength && address.Length >= MinLength))
            {
                return false;
            }

            return true;
        }
    }
}
