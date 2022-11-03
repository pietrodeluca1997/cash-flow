using CF.Core.DomainObjects;
using CF.Core.Helpers;
using CF.CustomMediator.Models;

namespace CF.Account.API.Commands.AccountManagerCommands
{
    public class CreateNewAccountManagerCommand : Command
    {
        public string Name { get; set; }
        public string CPFNumber { get; set; }
        public Guid AccountIdentifier { get; set; }
        public Guid UserId { get; set; }

        public CreateNewAccountManagerCommand()
        {

        }

        public CreateNewAccountManagerCommand(string name, string cpfNumber, Guid userId, Guid accountIdentifier)
        {
            Name = name;
            CPFNumber = cpfNumber.Replace(".", string.Empty).Replace("-", string.Empty).Trim();
            AccountIdentifier = accountIdentifier;
            UserId = userId;
        }

        public override bool IsValid()
        {
            bool isCpfNumberValid = CPF.IsValid(CPFNumber, out IList<string> cpfErrors);

            bool isNameValid = BasicValidationHelper.IsStringValid(Name, 3, 50, nameof(Name), out IList<string> nameErrors);

            if (!isCpfNumberValid) ValidationResult.AddError(cpfErrors, CPFNumber, nameof(CPFNumber));

            if (!isNameValid) ValidationResult.AddError(nameErrors, Name, nameof(Name));

            return !ValidationResult.ValidationFailures.Any();
        }
    }
}
