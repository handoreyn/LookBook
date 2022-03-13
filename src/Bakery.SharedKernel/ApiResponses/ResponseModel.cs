using System.Net;

public class ResponseModel<TDto> where TDto : DtoBase
{
    public HttpStatusCode StatusCode { get; private set; }
    public string Message { get; private set; }
    public TDto Result { get; private set; }

    public ResponseModel(HttpStatusCode statusCode, string message, TDto result = null)
    {
        StatusCode = statusCode;
        Message = message;
        Result = result;
    }
}