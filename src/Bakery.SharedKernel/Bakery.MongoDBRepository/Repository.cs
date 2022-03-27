using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Bakery.MongoDBRepository;

public abstract class Repository<TEntity> : DatabaseContext<TEntity>, IRepository<TEntity> where TEntity : class
{
    protected readonly FilterDefinitionBuilder<TEntity> Filter = Builders<TEntity>.Filter;
    protected Repository(IConfiguration configuration, string collectionName) : base(configuration, collectionName)
    {
    }

    public TEntity Find() => Collection
        .Find(FilterDefinition<TEntity>.Empty)
        .FirstOrDefault();

    public Task<TEntity> FindAsync() => Collection
        .Find(FilterDefinition<TEntity>.Empty)
        .FirstOrDefaultAsync();

    public TEntity Find(FilterDefinition<TEntity> filter) => Collection.Find(filter).FirstOrDefault();

    public Task<TEntity> FindAsync(FilterDefinition<TEntity> filter) => Collection.Find(filter).FirstOrDefaultAsync();

    public IEnumerable<TEntity> FindMany(FilterDefinition<TEntity> filter, int limit = 10, int page = 0) => Collection
        .Find(filter).Skip(page * limit).Limit(limit).ToList();

    public Task<List<TEntity>> FindManyAsync(FilterDefinition<TEntity> filter, int limit = 10, int page = 0) =>
        Collection.Find(filter).Skip(page * limit).Limit(limit).ToListAsync();

    public List<TEntity> FindMany(FilterDefinition<TEntity> filter, SortDefinition<TEntity> sort, int limit = 10,
        int page = 0) => Collection.Find(filter).Sort(sort).Skip(page * limit).Limit(limit).ToList();

    public Task<List<TEntity>> FindManyAsync(FilterDefinition<TEntity> filter, SortDefinition<TEntity> sort,
        int limit = 10, int page = 0) =>
        Collection.Find(filter).Sort(sort).Skip(page * limit).Limit(limit).ToListAsync();

    public TEntity Create(TEntity T)
    {
        Collection.InsertOne(T);
        return T;
    }

    public async Task<TEntity> CreateAsync(TEntity T)
    {
        await Collection.InsertOneAsync(T);
        return T;
    }

    public void Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions? options = null) =>
        Collection.UpdateOne(filter, update, options);

    public Task UpdateAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update,
        UpdateOptions? options = null) => Collection.UpdateOneAsync(filter, update, options);

    public void UpdateMany(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions? options = null) =>
        Collection.UpdateMany(filter, update, options);

    public Task UpdateManyAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions? options = null) =>
        Collection.UpdateManyAsync(filter, update, options);

    public void Delete(FilterDefinition<TEntity> filter) =>
        Collection.DeleteOne(filter);

    public Task DeleteAsync(FilterDefinition<TEntity> filter) => Collection.DeleteOneAsync(filter);

    public void DeleteMany(FilterDefinition<TEntity> filter) => Collection.DeleteMany(filter);

    public Task DeleteManyAsync(FilterDefinition<TEntity> filter) => Collection.DeleteManyAsync(filter);
}