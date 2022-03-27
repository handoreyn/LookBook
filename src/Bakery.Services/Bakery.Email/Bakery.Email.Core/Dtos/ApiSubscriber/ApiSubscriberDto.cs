namespace Bakery.Email.Core.Dtos.ApiSubscriber;

public class ApiSubscriberDto : DtoBase
{
    public string SubscriberId { get; set; }
    public string Email { get; set; }
    public string ApiKey { get; set; }
    public StatusEnumType Status { get; set; }


    public ApiSubscriberDto()
    {

    }
    public ApiSubscriberDto(string email, string apiKey, StatusEnumType status, string subscriberId)
    {
        Email = email;
        ApiKey = apiKey;
        Status = status;
        SubscriberId = subscriberId;
    }
}