using CF.CustomMediator.Models.Enums;
using System.Net;

namespace CF.CustomMediator.Strategies
{
    public class RequestBehaviourResponseStrategy
    {
        public readonly static IDictionary<ECommandResponseType, string> InformationMapperByCommandResponseType = new Dictionary<ECommandResponseType, string>
        {
            { ECommandResponseType.Created, "Created with success." },
            { ECommandResponseType.Updated, "Updated with success." },
            { ECommandResponseType.Deleted, "Deleted with success." },
            { ECommandResponseType.InvalidRequestData, "The information sent to the server was unprocessable." },
            { ECommandResponseType.NotFound, "The requested information was not found." },
            { ECommandResponseType.BusinessRuleConflict, "Some problems ocurred during validation with business rules." },
            { ECommandResponseType.InternalCommandError, "A internal problem has ocurred during the processament." }
        };

        public readonly static IDictionary<ECommandResponseType, HttpStatusCode> StatusCodeMapperByCommandResponseType = new Dictionary<ECommandResponseType, HttpStatusCode>
        {
            { ECommandResponseType.Created, HttpStatusCode.Created },
            { ECommandResponseType.Updated, HttpStatusCode.NoContent },
            { ECommandResponseType.Deleted, HttpStatusCode.NoContent },
            { ECommandResponseType.InvalidRequestData, HttpStatusCode.UnprocessableEntity },
            { ECommandResponseType.NotFound, HttpStatusCode.NotFound },
            { ECommandResponseType.BusinessRuleConflict, HttpStatusCode.Conflict },
            { ECommandResponseType.InternalCommandError, HttpStatusCode.InternalServerError }
        };
    }
}
