using AutoMapper;
using UrbanWatchAPI.Application.PublicTransport.Vehicles.DTOs;
using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;

namespace UrbanWatchAPI.Application.PublicTransport.Vehicles.Mapping;

public class RoutesMappingProfile : Profile
{
    public RoutesMappingProfile()
    {
        CreateMap<VehicleDto, Vehicle>().ReverseMap();
        CreateMap<VehicleSnapshotDto, VehicleSnapshot>().ReverseMap();
    }
}