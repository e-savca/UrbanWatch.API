using MongoDB.Driver;
using Microsoft.Extensions.Options;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo;

public class MongoContext
{
    private readonly IMongoDatabase _database;

    public MongoContext(IOptions<MongoSettings> mongoSettings)
    {
        var settings = mongoSettings.Value;

        var client = new MongoClient($"mongodb://{settings.Username}:{settings.Password}@{settings.Host}:{settings.Port}");
        _database = client.GetDatabase(settings.Database);
    }
    public IMongoCollection<VehicleSnapshotDocument> VehicleHistory =>
        _database.GetCollection<VehicleSnapshotDocument>("vehicles_live");
    public IMongoCollection<RouteDocument> Routes => 
        _database.GetCollection<RouteDocument>("routes");
    public IMongoCollection<ShapeDocument> Shapes => 
        _database.GetCollection<ShapeDocument>("shapes");
    public IMongoCollection<StopDocument> Stops => 
        _database.GetCollection<StopDocument>("stops");
    public IMongoCollection<StopTimesDocument> StopTimes => 
        _database.GetCollection<StopTimesDocument>("stop_times");
    public IMongoCollection<TripDocument> Trips => 
        _database.GetCollection<TripDocument>("trips");
}