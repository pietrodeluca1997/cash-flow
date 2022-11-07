using System;
using Xunit;

namespace CF.Domain.Tests
{
    public class AccountTests
    {
        [Fact(DisplayName = "Create an account with valid amount of money")]
        [Trait("Category", "Account Creation")]
        public void Account_PositiveAmountOfInitialCredit_ShouldBeCreatedWithSuccess()
        {
            decimal initialValue = 200m;

            Account.API.Entities.Account account = new(initialValue);

            Assert.Equal(account.MoneyAmount, initialValue);
        }

        [Fact(DisplayName = "Create an account with zero amount of money")]
        [Trait("Category", "Account Creation")]
        public void Account_ZeroAmountOfInitialCredit_ShouldBeCreatedWithSuccess()
        {
            decimal initialValue = 0m;

            Account.API.Entities.Account account = new(initialValue);

            Assert.Equal(account.MoneyAmount, initialValue);
        }

        [Fact(DisplayName = "Create an account with invalid amount of money")]
        [Trait("Category", "Account Creation")]
        public void Account_NegativeAmountOfInitialCredit_ShouldReturnException()
        {           
            Assert.Throws<ArgumentException>(() =>
            {
                decimal initialValue = -100m;
                Account.API.Entities.Account account = new(initialValue);
            });
        }

        [Fact(DisplayName = "Add valid credit amount into an account")]
        [Trait("Category", "Account Credit")]
        public void Account_ValidAmountOfCredit_ShouldCreditWithSuccess()
        {
            decimal initialValue = 200m;
            decimal creditValue = 300m;

            Account.API.Entities.Account account = new(initialValue);

            account.Credit(creditValue);

            Assert.Equal(account.MoneyAmount, initialValue + creditValue);
        }

        [Fact(DisplayName = "Try to add invalid credit amount into an account")]
        [Trait("Category", "Account Credit")]
        public void Account_InvalidAmountOfCredit_ShouldNotCredit()
        {
            decimal initialValue = 200m;
            decimal creditValue = -100m;

            Account.API.Entities.Account account = new(initialValue);

            bool wasSuccesful = account.Credit(creditValue);

            Assert.True(!wasSuccesful);
        }

        [Fact(DisplayName = "Remove valid debit amount from an account")]
        [Trait("Category", "Account Debit")]
        public void Account_ValidAmountOfDebit_ShouldDebitWithSuccess()
        {
            decimal initialValue = 200m;
            decimal debitValue = 100m;

            Account.API.Entities.Account account = new(initialValue);

            account.Debit(debitValue);

            Assert.Equal(account.MoneyAmount, initialValue - debitValue);
        }

        [Fact(DisplayName = "Try to remove debit amount from an account bigger than available")]
        [Trait("Category", "Account Debit")]
        public void Account_AmountOfDebitMoreThanAvailable_ShouldNotDebit()
        {
            decimal initialValue = 200m;
            decimal debitValue = 400m;

            Account.API.Entities.Account account = new(initialValue);

            bool wasSuccesful = account.Debit(debitValue);

            Assert.True(!wasSuccesful);
        }
    }
}