using MongoDB.Driver;
using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Repositories;

public class StopRepository(MongoContext mongoContext) : BaseRepository<StopDocument, int>(mongoContext.Stops)
{
    public override Task<StopDocument?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public override Task<StopDocument> UpdateAsync(StopDocument entity)
    {
        throw new NotImplementedException();
    }

    public override Task<StopDocument?> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}