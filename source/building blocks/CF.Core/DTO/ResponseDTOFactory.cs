using CF.CustomMediator.Contracts;
using CF.CustomMediator.Models;
using CF.CustomMediator.Strategies;
using System.Net;

namespace CF.Core.DTO
{
    public class ResponseDTOFactory
    {
        public static BaseResponseDTO CreateFromRequestBehaviour(IRequestBehaviourContext requestBehaviourContext)
        {
            BaseResponseDTO responseDTO;

            string message = RequestBehaviourResponseStrategy.InformationMapperByCommandResponseType[requestBehaviourContext.CommandResponseType];
            HttpStatusCode statusCode = RequestBehaviourResponseStrategy.StatusCodeMapperByCommandResponseType[requestBehaviourContext.CommandResponseType];

            if (!requestBehaviourContext.RequestMessage.ValidationResult.ValidationFailures.Any() && (int)statusCode >= 200 && (int)statusCode <= 299)
            {
                responseDTO = new SuccessResponseDTO(statusCode, message);
            }
            else if (requestBehaviourContext.CustomCommandResponse is not null)
            {
                responseDTO = new ErrorResponseDTO<object>(statusCode,
                                                           message,
                                                           requestBehaviourContext.CustomCommandResponse);
            }
            else
            {
                responseDTO = new ErrorResponseDTO<ValidationResult>(statusCode,
                                                                            message,
                                                                            requestBehaviourContext.RequestMessage.ValidationResult);
            }

            return responseDTO;
        }
    }
}
