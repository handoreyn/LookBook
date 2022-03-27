using Bakery.Email.Core.Dtos.ApiSubscriber;
using Bakery.Email.Core.Entities;
using Bakery.Email.Core.Repository;
using Bakery.MongoDBRepository;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;

namespace Bakery.Email.Infrastructure.Repository;

public class ApiSubscriberRepository : Repository<ApiSubscriber>, IApiSubscriberRepository
{
    public ApiSubscriberRepository(IConfiguration configuration) : base(configuration, "api_subscribers")
    {
    }

    public async Task<ApiSubscriberDto> CreateApiSubscriberAsync(CreateApiSubscriberDto model)
    {
        var apiSubscriber = new ApiSubscriber
        {
            Email = model.Email,
            ApiKey = Guid.NewGuid().ToString(),
            Status = StatusEnumType.active
        };

        await CreateAsync(apiSubscriber);

        return new ApiSubscriberDto
        {
            ApiKey = apiSubscriber.ApiKey,
            Email = apiSubscriber.Email,
            Status = apiSubscriber.Status
        };
    }

    public async Task<ApiSubscriberDto> FindSubscriberByEmailAsync(string email)
    {
        var query = Filter.Eq(l => l.Email, email);
        var apiSubscriber = await FindAsync(query);

        if (apiSubscriber == null) return null;

        return new ApiSubscriberDto(apiSubscriber.Email, apiSubscriber.ApiKey,
            apiSubscriber.Status, apiSubscriber.Id.ToString());
    }

    public async Task<ApiSubscriberDto> FindSubscriberByIdAsync(string id)
    {
        var query = Filter.Eq(l => l.Id, ObjectId.Parse(id));
        var apiSubscriber = await FindAsync(query);

        if (apiSubscriber == null) return null;

        return new ApiSubscriberDto(apiSubscriber.Email, apiSubscriber.ApiKey,
                    apiSubscriber.Status, apiSubscriber.Id.ToString());
    }

    public async Task<IEnumerable<ApiSubscriberDto>> GetApiSubscribers(int from = 0, int pageSize = 10)
    {
        var subscribers = await FindManyAsync(Filter.Empty, pageSize, from);

        return subscribers?
            .Select(t => new ApiSubscriberDto(t.Email, t.ApiKey, t.Status, t.Id.ToString()));
    }

    public async Task<bool> IsApiKeyValidAsync(string apiKey)
    {
        var query = Filter.And(Filter.Eq(l => l.ApiKey, apiKey),
            Filter.Eq(l => l.Status, StatusEnumType.active));

        var count = await Collection.CountDocumentsAsync(query);
        return count > 0;
    }
}