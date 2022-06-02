using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Bakery.MongoDBRepository;

public abstract class DatabaseContext<TEntity> where TEntity : class
{
    protected readonly IMongoCollection<TEntity> Collection;

    protected DatabaseContext(IMongoClient client, IConfiguration configuration, string collectionName)
    {
        var database = client.GetDatabase(configuration["MongoDbName"]);
        Collection = database.GetCollection<TEntity>(collectionName);
    }
}