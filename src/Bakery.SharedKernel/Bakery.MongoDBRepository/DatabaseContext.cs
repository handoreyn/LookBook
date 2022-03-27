using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Bakery.MongoDBRepository;

public abstract class DatabaseContext<TEntity> where TEntity : class
{
    protected readonly IMongoCollection<TEntity> Collection;

    protected DatabaseContext(IConfiguration configuration, string collectionName)
    {
        var clientSettings = new MongoClientSettings
        {
            Server = new MongoServerAddress(configuration["MongoHost"], int.Parse(configuration["MongoPort"])),
            Credential = MongoCredential.CreateCredential(databaseName: configuration["MongoAuthDbName"],
                username: configuration["MongoDbUsername"],
                password: configuration["MongoDbPassword"])
        };

        var client = new MongoClient(clientSettings);
        var database = client.GetDatabase(configuration["MongoDbName"]);
        Collection = database.GetCollection<TEntity>(collectionName);
    }
}