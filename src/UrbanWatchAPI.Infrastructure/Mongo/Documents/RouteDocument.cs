using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UrbanWatchAPI.Infrastructure.Interfaces;

namespace UrbanWatchAPI.Infrastructure.Mongo.Documents;

public class RouteDocument : IDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string? AgencyId { get; set; }
    public int RouteId { get; set; }
    public string? RouteShortName { get; set; }
    public string? RouteLongName { get; set; }
    public string? RouteColor { get; set; }
    public int RouteType { get; set; }
    public string? RouteDesc { get; set; }
}

