using Bakery.Email.Core.Dtos.ApiSubscriber;
using Bakery.Email.Core.Entities;
using Bakery.MongoDBRepository;

namespace Bakery.Email.Core.Repository;

public interface IApiSubscriberRepository : IRepository<ApiSubscriber>
{
    Task<ApiSubscriberDto> FindSubscriberByEmailAsync(string email);
    Task<bool> IsApiKeyValidAsync(string apiKey);
    Task<ApiSubscriberDto> CreateApiSubscriberAsync(CreateApiSubscriberDto model);
    Task<ApiSubscriberDto> FindSubscriberByIdAsync(string id);
    Task<IEnumerable<ApiSubscriberDto>> GetApiSubscribers(int from = 0, int pageSize = 10);
}