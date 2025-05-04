using MongoDB.Driver;
using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Repositories;

public class RouteRepository(MongoContext mongoContext) : BaseRepository<RouteDocument, int>(mongoContext.Routes)
{
    public override async Task<RouteDocument?> GetByIdAsync(int id)
    {
        return await Collection.Find(x => x.RouteId == id).FirstOrDefaultAsync();
    }

    public override async Task<RouteDocument> UpdateAsync(RouteDocument entity)
    {
        await Collection.ReplaceOneAsync(x => x.RouteId == entity.RouteId, entity);
        return entity;
    }
    public override async Task<RouteDocument?> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            await Collection.DeleteOneAsync(x => x.RouteId == id);
        }
        return entity;
    }

}