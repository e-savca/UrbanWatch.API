using AutoMapper;
using UrbanWatchAPI.Application.PublicTransport.Vehicles.DTOs;
using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Application.PublicTransport.Vehicles.Mapping;

public class RoutesMappingProfile : Profile
{
    public RoutesMappingProfile()
    {
        CreateMap<VehicleDto, Vehicle>().ReverseMap();
        CreateMap<VehicleSnapshotDto, VehicleSnapshot>().ReverseMap();

        CreateMap<VehicleDto, VehicleDocument>().ReverseMap();
        CreateMap<VehicleSnapshotDto, VehicleSnapshotDocument>().ReverseMap();
    }
}