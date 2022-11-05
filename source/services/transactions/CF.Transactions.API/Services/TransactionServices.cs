using CF.Core.DomainObjects;
using CF.Core.DTO;
using CF.Core.Helpers;
using CF.Core.Messages.IntegrationEvents;
using CF.CustomMediator.Contracts;
using CF.Transactions.API.Contracts.Services;
using CF.Transactions.API.DTO.Request;
using CF.Transactions.API.Entities;
using System.Net;

namespace CF.Transactions.API.Services
{
    public class TransactionServices : ITransactionServices
    {
        private readonly IMediator _mediator;
        private readonly RequestHandler _requestHandler;

        public TransactionServices(IMediator mediator, RequestHandler requestHandler)
        {
            _mediator = mediator;
            _requestHandler = requestHandler;
        }

        public async Task<BaseResponseDTO> Credit(CreateNewTransactionRequestDTO createTransactionRequestDTO)
        {
            BaseResponseDTO responseDTO;
            try
            {
                bool isTransactionValid = Transaction.IsValid(createTransactionRequestDTO.MoneyAmount, out IList<string> transactionErrors);

                if (!isTransactionValid)
                {
                    responseDTO = new ErrorResponseDTO<IList<string>>(HttpStatusCode.UnprocessableEntity, "Transaction information is not valid", transactionErrors);
                    return responseDTO;
                }

                string authorizationHeader = _requestHandler.GetHeaderValue("Authorization");

                User user = JwtHelper.RetrieveUserData(authorizationHeader.Replace("Bearer ", ""));

                CreditTransactionRequestedEvent @event = new()
                {
                    Description = createTransactionRequestDTO.Description,
                    MoneyAmount = createTransactionRequestDTO.MoneyAmount,
                    Email = user.Email,
                    UserId = user.UserId
                };

                await _mediator.Notify(@event);

                responseDTO = new SuccessResponseDTO(HttpStatusCode.Accepted, "Credit transaction accepted for further processment");
            }
            catch (Exception ex)
            {
                responseDTO = new ErrorResponseDTO(HttpStatusCode.InternalServerError, $"Internal error. Cause: {ex.Message}");
            }

            return responseDTO;
        }

        public async Task<BaseResponseDTO> Debit(CreateNewTransactionRequestDTO createTransactionRequestDTO)
        {
            BaseResponseDTO responseDTO;
            try
            {
                bool isTransactionValid = Transaction.IsValid(createTransactionRequestDTO.MoneyAmount, out IList<string> transactionErrors);

                if (!isTransactionValid)
                {
                    responseDTO = new ErrorResponseDTO<IList<string>>(HttpStatusCode.UnprocessableEntity, "Transaction information is not valid", transactionErrors);
                    return responseDTO;
                }

                string authorizationHeader = _requestHandler.GetHeaderValue("Authorization");

                User user = JwtHelper.RetrieveUserData(authorizationHeader.Replace("Bearer ", ""));

                DebitTransactionRequestedEvent @event = new()
                {
                    Description = createTransactionRequestDTO.Description,
                    MoneyAmount = createTransactionRequestDTO.MoneyAmount,
                    Email = user.Email,
                    UserId = user.UserId
                };

                await _mediator.Notify(@event);

                responseDTO = new SuccessResponseDTO(HttpStatusCode.Accepted, "Debit transaction accepted for further processment");
            }
            catch (Exception ex)
            {
                responseDTO = new ErrorResponseDTO(HttpStatusCode.InternalServerError, $"Internal error. Cause: {ex.Message}");
            }

            return responseDTO;
        }
    }
}
