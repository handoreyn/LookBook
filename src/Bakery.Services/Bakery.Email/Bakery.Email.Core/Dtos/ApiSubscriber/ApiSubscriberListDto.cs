namespace Bakery.Email.Core.Dtos.ApiSubscriber;

public class ApiSubscriberListDto : DtoBase
{
    public IEnumerable<ApiSubscriberDto> Items;

    public ApiSubscriberListDto(IEnumerable<ApiSubscriberDto> items)
    {
        Items = items;   
    }
}