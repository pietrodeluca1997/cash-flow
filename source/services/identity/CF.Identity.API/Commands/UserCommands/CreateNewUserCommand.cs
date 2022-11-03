using CF.Core.DomainObjects;
using CF.Core.Helpers;
using CF.CustomMediator.Models;
using CF.Identity.API.Models;

namespace CF.Identity.API.Commands.UserCommands
{
    public class CreateNewUserCommand : Command
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string CPFNumber { get; set; }
        public string Name { get; set; }

        public CreateNewUserCommand()
        {

        }

        public CreateNewUserCommand(string email, string password, string passwordConfirmation, string cPFNumber, string name)
        {
            Email = email;
            Password = password;
            PasswordConfirmation = passwordConfirmation;
            CPFNumber = cPFNumber;
            Name = name;
        }

        public override bool IsValid()
        {
            bool isEmailValid = Core.DomainObjects.Email.IsValid(Email, out IList<string> emailErrors);

            bool isPasswordValid = UserPassword.IsValidForCreation(Password, PasswordConfirmation, out IList<string> passwordErrors);

            bool isCpfNumberValid = CPF.IsValid(CPFNumber, out IList<string> cpfErrors);

            bool isNameValid = BasicValidationHelper.IsStringValid(Name, 3, 50, nameof(Name), out IList<string> nameErrors);

            if (!isEmailValid) ValidationResult.AddError(emailErrors, Email, nameof(Email));

            if (!isPasswordValid) ValidationResult.AddError(passwordErrors, Password, nameof(Password));

            if (!isCpfNumberValid) ValidationResult.AddError(cpfErrors, CPFNumber, nameof(CPFNumber));

            if (!isNameValid) ValidationResult.AddError(nameErrors, Name, nameof(Name));

            return !ValidationResult.ValidationFailures.Any();
        }
    }
}
