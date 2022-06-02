using Bakery.Member.Core.Repository;
using Bakery.Member.Infrastructure.Repository;
using MongoDB.Driver;

namespace Bakery.Member.Api.ServiceExtensions;

public static class RepositoryServiceExtension
{
    public static void AddRepository(this IServiceCollection services, IConfiguration configuration) =>

        services
        .AddSingleton<IMongoClient>(new MongoClient(
            new MongoClientSettings
            {
                Server = new MongoServerAddress(configuration["MongoHost"], int.Parse(configuration["MongoPort"] ?? "27017")),
                Credential = MongoCredential.CreateCredential(databaseName: configuration["MongoAuthDbName"],
                username: configuration["MongoDbUsername"],
                password: configuration["MongoDbPassword"])
            }
        ))
        .AddScoped<IMemberRepository, MemberRepository>();
}