public class ResponseModel<TDto> where TDto : DtoBase
{
    public string Message { get; private set; }
    public TDto Result { get; private set; }

    public ResponseModel(string message, TDto result = null)
    {
        Message = message;
        Result = result;
    }
}