using System.Net;

namespace CF.Core.DTO
{
    public class SuccessResponseDTO : BaseResponseDTO
    {
        public override bool Succeeded { get; }

        public SuccessResponseDTO(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
            Succeeded = true;
        }
    }

    public class SuccessResponseDTO<TResponseDTO> : SuccessResponseDTO where TResponseDTO : class
    {
        public TResponseDTO Data { get; set; }

        public SuccessResponseDTO(HttpStatusCode statusCode, string message, TResponseDTO data) : base(statusCode, message)
        {
            Data = data;
        }
    }
}
