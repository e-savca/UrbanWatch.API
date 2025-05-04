using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UrbanWatchAPI.Infrastructure.Interfaces;

namespace UrbanWatchAPI.Infrastructure.Mongo.Documents;

public class ShapeDocument : IDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string? ShapeId { get; set; }
    public double ShapePtLat { get; set; }
    public double ShapePtLon { get; set; }
    public int ShapePtSequence { get; set; }
}

