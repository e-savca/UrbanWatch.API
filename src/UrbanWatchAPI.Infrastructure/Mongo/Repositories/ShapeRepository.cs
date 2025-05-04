using MongoDB.Driver;
using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Repositories;

public class ShapeRepository(MongoContext mongoContext) : BaseRepository<ShapeDocument, string>(mongoContext.Shapes)
{
    public override Task<ShapeDocument?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public override Task<ShapeDocument> UpdateAsync(ShapeDocument entity)
    {
        throw new NotImplementedException();
    }

    public override Task<ShapeDocument?> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }
}