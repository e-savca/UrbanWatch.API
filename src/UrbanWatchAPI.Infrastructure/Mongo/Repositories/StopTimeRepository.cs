using MongoDB.Driver;
using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Repositories;

public class StopTimeRepository(MongoContext mongoContext)
{
    private readonly IMongoCollection<StopTimesDocument> _collection = mongoContext.StopTimes;
}