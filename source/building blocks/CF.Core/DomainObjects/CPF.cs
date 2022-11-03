namespace CF.Core.DomainObjects
{
    public class CPF
    {
        public const int TotalLength = 11;
        public const int FirstPartLength = 9;
        public const int DigitLength = 2;

        public string Number { get; private set; }

        public CPF(string number)
        {
            Number = string.Empty;

            if (!IsValid(number)) return;

            Number = number;
        }

        public static bool IsValid(string number, out IList<string> validationErrors)
        {
            validationErrors = new List<string>();

            if (string.IsNullOrWhiteSpace(number))
            {
                validationErrors.Add("CPF should be informed and cannot be null or empty");
            }

            if (!CalculateCPF(number))
            {
                validationErrors.Add($"CPF should be valid");
            }

            return !validationErrors.Any();
        }

        public static bool IsValid(string number)
        {
            if (string.IsNullOrWhiteSpace(number)) return false;

            return CalculateCPF(number);
        }

        public static bool CalculateCPF(string number)
        {
            //Multipliers
            int[] firstMultipliers = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] secondMultipliers = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            //Clear whitespace and possible unwanted characters from cpf number
            number = number.Trim();
            number = number.Replace(".", string.Empty).Replace("-", string.Empty);

            //Check CPF length
            if (number.Length != TotalLength) return false;

            //Get the first part of cpf number
            string numberFirstPart = number[..FirstPartLength];

            int sum = 0;

            for (int i = 0; i < FirstPartLength; i++)
            {
                sum += int.Parse(numberFirstPart[i].ToString()) * firstMultipliers[i];
            }

            int rest = sum % TotalLength;

            if (rest < 2) rest = 0;
            else
                rest = TotalLength - rest;

            string digit = rest.ToString();

            numberFirstPart += digit;

            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(numberFirstPart[i].ToString()) * secondMultipliers[i];
            }

            rest = sum % TotalLength;

            if (rest < 2) rest = 0;
            else
                rest = TotalLength - rest;

            digit += rest.ToString();

            return number.EndsWith(digit);
        }
    }
}