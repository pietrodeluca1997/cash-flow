using CF.Core.DTO;

namespace CF.Transactions.API.DTO.Request
{
    public class CreateNewTransactionRequestDTO : BaseRequestDTO
    {
        public decimal MoneyAmount { get; set; }
        public string Description { get; set; }

        public CreateNewTransactionRequestDTO()
        {

        }

        public CreateNewTransactionRequestDTO(decimal moneyAmount, string description)
        {
            MoneyAmount = moneyAmount;
            Description = description;
        }
    }
}
