using System.Net;

namespace CF.Core.DTO
{
    public class ErrorResponseDTO : BaseResponseDTO
    {
        public override bool Succeeded { get; }

        public ErrorResponseDTO(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
            Succeeded = false;
        }
    }

    public class ErrorResponseDTO<TResponseDTO> : ErrorResponseDTO where TResponseDTO : class
    {
        public TResponseDTO Data { get; set; }
        public ErrorResponseDTO(HttpStatusCode statusCode, string message, TResponseDTO data) : base(statusCode, message)
        {
            Data = data;
        }
    }
}
