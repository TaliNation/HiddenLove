using System.Net;

namespace HiddenLove.Shared.Models
{
    public record HttpError
    {
        public HttpStatusCode StatusCode { get; init; }
        public string Message { get; init; }

        public HttpError(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}