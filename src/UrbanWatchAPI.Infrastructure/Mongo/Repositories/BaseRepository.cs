using MongoDB.Driver;
using UrbanWatchAPI.Infrastructure.Interfaces;

namespace UrbanWatchAPI.Infrastructure.Mongo.Repositories;

public abstract class BaseRepository<T, TId>(IMongoCollection<T> collection)
{
    protected readonly IMongoCollection<T> Collection = collection;

    public abstract Task<T?> GetByIdAsync(TId id);
    public async Task<List<T>> GetAllAsync()
    {
        return await Collection.Find(_ => true).ToListAsync();
    }

    public async Task<T> InsertAsync(T entity)
    {
        await Collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<List<T>> InsertBatchAsync(IEnumerable<T> entities)
    {
        var list = entities.ToList();
        if (list.Count > 0)
        {
            await Collection.InsertManyAsync(list);
        }
        return list;
    }

    public abstract Task<T> UpdateAsync(T entity);

    public abstract Task<T?> DeleteAsync(TId id);

    public async Task<long> DeleteAllAsync()
    {
        var result = await Collection.DeleteManyAsync(_ => true);
        return result.DeletedCount;
    }
}