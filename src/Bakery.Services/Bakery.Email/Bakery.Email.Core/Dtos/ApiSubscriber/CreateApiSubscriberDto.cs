using Bakery.SharedKernel.Dtos;

namespace Bakery.Email.Core.Dtos.ApiSubscriber;

public class CreateApiSubscriberDto : DtoBase
{
    public string Email { get; set; }
}