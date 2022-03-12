using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

public abstract class DatabaseContext<TEntity> where TEntity : class
{
    protected readonly IMongoCollection<TEntity> Collection;

    protected DatabaseContext(IConfiguration configuration)
    {
        var clientSettings = new MongoClientSettings
        {
            Server = new MongoServerAddress(configuration["MongoHost"], int.Parse(configuration["MongoPort"])),
            Credential = MongoCredential.CreateCredential(databaseName: configuration["MongoDbName"],
                username: configuration["MongoDbUsername"],
                password: configuration["MongoDbPassword"])
        };

        var client = new MongoClient(clientSettings);
        var database = client.GetDatabase(configuration["MongoDbName"]);
        Collection = database.GetCollection<TEntity>(nameof(TEntity).ToLower());
    }
}