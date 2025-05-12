using MongoDB.Bson;
using MongoDB.Driver;
using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Repositories;

public class VehicleSnapshotRepository(MongoContext mongoContext)
{
    private readonly IMongoCollection<VehicleSnapshotDocument> _collection = mongoContext.VehicleHistory;

    public async Task<List<VehicleDocument>> GetLastSnapshotAsync()
    {
        var last = await _collection
            .Find(Builders<VehicleSnapshotDocument>.Filter.Empty)
            .SortByDescending(x => x.Timestamp)
            .Limit(1)
            .FirstOrDefaultAsync();
        return last.Vehicles;
    }

    public async Task<VehicleSnapshotDocument> InsertAsync(VehicleSnapshotDocument entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<int> ClearAsync(TimeSpan timeSpan, CancellationToken ct)
    {
        var threshold = DateTime.UtcNow - timeSpan;
        var filter = Builders<VehicleSnapshotDocument>.Filter.Lt(x => x.Timestamp, threshold);
        var result = await _collection.DeleteManyAsync(filter, cancellationToken: ct);
        return Int32.Parse(result.DeletedCount.ToString());
    }
}