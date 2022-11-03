namespace CF.Identity.API.Models
{
    public class UserPassword
    {
        public const int MaxLength = 50;
        public const int MinLength = 6;
        public string Password { get; private set; }

        public UserPassword(string password, string passwordConfirmation)
        {
            Password = string.Empty;

            if (IsValidForCreation(password, passwordConfirmation))
            {
                Password = password;
            }
        }
        public static bool IsValidForCreation(string password, string passwordConfirmation, out IList<string> validationErrors)
        {
            validationErrors = new List<string>();

            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordConfirmation))
            {
                validationErrors.Add("Password and password confirmation should be informed and cannot be null or empty");
            }

            if (!(password.Length <= MaxLength && password.Length >= MinLength && password.Equals(passwordConfirmation)))
            {
                validationErrors.Add($"Password and password confirmation should be valid and have between {MinLength} and {MaxLength} characters");
            }

            return !validationErrors.Any();
        }

        public static bool IsValidForCreation(string password, string passwordConfirmation)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordConfirmation))
            {
                return false;
            }

            if (!(password.Length <= MaxLength && password.Length >= MinLength && password.Equals(passwordConfirmation)))
            {
                return false;
            }

            return true;
        }
    }
}
