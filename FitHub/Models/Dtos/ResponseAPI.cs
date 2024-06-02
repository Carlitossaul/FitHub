using System.Net;

namespace FitHub.Models.Dtos
{
    public class ResponseAPI
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }

    }
}
