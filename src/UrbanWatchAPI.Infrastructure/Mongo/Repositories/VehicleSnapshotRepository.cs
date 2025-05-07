using MongoDB.Driver;
using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Repositories;

public class VehicleSnapshotRepository(MongoContext mongoContext)
{
    private readonly IMongoCollection<VehicleSnapshotDocument> _collection = mongoContext.VehicleHistory;
    
    public async Task<VehicleSnapshotDocument> InsertAsync(VehicleSnapshotDocument entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }
}