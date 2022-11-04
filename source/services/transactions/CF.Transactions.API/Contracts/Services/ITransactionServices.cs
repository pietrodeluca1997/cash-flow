using CF.Core.DTO;
using CF.Transactions.API.DTO.Request;

namespace CF.Transactions.API.Contracts.Services
{
    public interface ITransactionServices
    {
        Task<BaseResponseDTO> Credit(CreateNewTransactionRequestDTO createTransactionRequestDTO);
        Task<BaseResponseDTO> Debit(CreateNewTransactionRequestDTO createTransactionRequestDTO);
    }
}
