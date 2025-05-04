using AutoMapper;
using MediatR;
using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Infrastructure.Mongo.Repositories;

namespace UrbanWatchAPI.Application.PublicTransport.Routes.Queries.GetAllRoutes;

public class GetAllRoutesQuery : IRequest<List<Route>> {}

public class GetAllRoutesQueryHandler(RouteRepository routeRepository, IMapper mapper) : IRequestHandler<GetAllRoutesQuery, List<Route>>
{
    public async Task<List<Route>> Handle(GetAllRoutesQuery request, CancellationToken cancellationToken)
    {
        var routesDocuments = await routeRepository.GetAllAsync();
        
        return mapper.Map<List<Route>>(routesDocuments);
    }
}