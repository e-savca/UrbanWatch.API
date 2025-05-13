using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using UrbanWatchAPI.Domain.Common.Exceptions;

namespace UrbanWatchAPI.Infrastructure.Mongo.Repositories;

public abstract class BaseRepository<T, TId>(IMongoCollection<T> collection, ILogger<BaseRepository<T, TId>> logger)
{
    protected readonly IMongoCollection<T> Collection = collection;

    public abstract Task<T?> GetByIdAsync(TId id);

    public async Task<List<T>> GetAllAsync()
    {
        logger.LogInformation("Retrieving all documents of type {Type}", typeof(T).Name);
        return await Collection.Find(_ => true).ToListAsync();
    }

    public async Task<T> InsertAsync(T entity)
    {
        logger.LogInformation("Inserting one document of type {Type}", typeof(T).Name);
        await Collection.InsertOneAsync(entity);
        logger.LogInformation("Insert successful for type {Type}", typeof(T).Name);
        return entity;
    }

    public async Task<List<T>> InsertBatchAsync(IEnumerable<T> entities)
    {
        var list = entities.ToList();
        logger.LogInformation("Inserting batch of {Count} documents of type {Type}", list.Count, typeof(T).Name);

        if (list.Count == 0)
        {
            logger.LogWarning("InsertBatchAsync called with empty list for type {Type}", typeof(T).Name);
            return list;
        }

        try
        {
            await Collection.InsertManyAsync(list);
            logger.LogInformation("Batch insert successful for type {Type}", typeof(T).Name);
            return list;
        }
        catch (MongoBulkWriteException ex)
        {
            logger.LogError(ex, "Bulk insert failed (MongoBulkWriteException) for type {Type}", typeof(T).Name);
            throw new MongoInsertBatchException("Mongo bulk insert failed", ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "InsertBatchAsync failed for type {Type}", typeof(T).Name);
            throw new MongoInsertBatchException("Unexpected error during batch insert", ex);
        }
    }

    public abstract Task<T> UpdateAsync(T entity);

    public abstract Task<T?> DeleteAsync(TId id);

    public async Task<long> DeleteAllAsync()
    {
        logger.LogWarning("Deleting ALL documents of type {Type}", typeof(T).Name);
        try
        {
            var result = await Collection.DeleteManyAsync(_ => true);
            logger.LogInformation("Deleted {Count} documents of type {Type}", result.DeletedCount, typeof(T).Name);
            return result.DeletedCount;
        }
        catch (Exception e)
        {
            logger.LogError(e, "DeleteAllAsync failed for type {Type}", typeof(T).Name);
            throw new MongoDeleteException($"Failed to delete all documents of type {typeof(T).Name}", e);
        }
    }
}
