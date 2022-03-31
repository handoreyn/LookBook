namespace Bakery.SharedKernel.ApiResponses;

public class ResponseModel<TDto> where TDto : DtoBase
{
    public string Message { get; }
    public TDto? Result { get; }

    public ResponseModel(string message)
    {
        Message = message;
    }

    public ResponseModel(string message, TDto result)
    {
        Message = message;
        Result = result;
    }
}