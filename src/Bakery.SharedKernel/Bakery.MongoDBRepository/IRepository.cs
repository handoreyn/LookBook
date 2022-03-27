using MongoDB.Driver;

namespace Bakery.MongoDBRepository;

public interface IRepository<TEntity> where TEntity : class
{
    TEntity Find();
    Task<TEntity> FindAsync();
    TEntity Find(FilterDefinition<TEntity> filter);
    Task<TEntity> FindAsync(FilterDefinition<TEntity> filter);
    IEnumerable<TEntity> FindMany(FilterDefinition<TEntity> filter, int limit = 10, int page = 0);
    Task<List<TEntity>> FindManyAsync(FilterDefinition<TEntity> filter, int limit = 10, int page = 0);
    List<TEntity> FindMany(FilterDefinition<TEntity> filter, SortDefinition<TEntity> sort, int limit = 10, int page = 0);
    Task<List<TEntity>> FindManyAsync(FilterDefinition<TEntity> filter, SortDefinition<TEntity> sort, int limit = 10, int page = 0);

    TEntity Create(TEntity T);
    Task<TEntity> CreateAsync(TEntity T);
    void Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options);
    Task UpdateAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options);
    void UpdateMany(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options);
    Task UpdateManyAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options);

    void Delete(FilterDefinition<TEntity> filter);
    Task DeleteAsync(FilterDefinition<TEntity> filter);
    void DeleteMany(FilterDefinition<TEntity> filter);
    Task DeleteManyAsync(FilterDefinition<TEntity> filter);
}