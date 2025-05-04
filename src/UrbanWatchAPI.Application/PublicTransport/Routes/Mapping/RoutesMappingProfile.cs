using AutoMapper;
using UrbanWatchAPI.Application.PublicTransport.Routes.DTOs;
using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Application.PublicTransport.Routes.Mapping;

public class RoutesMappingProfile : Profile
{
    public RoutesMappingProfile()
    {
        CreateMap<RouteDTO, Route>().ReverseMap();  
        CreateMap<RouteDocument, Route>().ReverseMap();  
    }
}