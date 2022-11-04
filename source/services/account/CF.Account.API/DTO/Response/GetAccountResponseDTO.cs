namespace CF.Account.API.DTO.Response
{
    public class GetAccountResponseDTO
    {
        public Guid UniqueIdentifier { get; set; }
        public int Id { get; set; }
        public decimal MoneyAmount { get; set; }

        public GetAccountResponseDTO()
        {

        }

        public GetAccountResponseDTO(Guid uniqueIdentifier, int id, decimal moneyAmount)
        {
            UniqueIdentifier = uniqueIdentifier;
            Id = id;
            MoneyAmount = moneyAmount;
        }
    }
}
