using System.Net;

namespace CF.Core.DTO
{
    public abstract class BaseResponseDTO
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public DateTime ResponseTime { get; set; }
        public abstract bool Succeeded { get; }

        protected BaseResponseDTO(HttpStatusCode statusCode,
                                  string message)
        {
            StatusCode = statusCode;
            Message = message;
            ResponseTime = DateTime.Now;
        }
    }
}
