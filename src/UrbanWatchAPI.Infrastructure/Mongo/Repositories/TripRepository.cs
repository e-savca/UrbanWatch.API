using MongoDB.Driver;
using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Repositories;

public class TripRepository(MongoContext mongoContext) : BaseRepository<TripDocument, string>(mongoContext.Trips)
{
    public override Task<TripDocument?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public override Task<TripDocument> UpdateAsync(TripDocument entity)
    {
        throw new NotImplementedException();
    }

    public override Task<TripDocument?> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }
}