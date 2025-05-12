using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using UrbanWatchAPI.Application.PublicTransport.Vehicles.DTOs;
using UrbanWatchAPI.Infrastructure.Mongo.Repositories;

namespace UrbanWatchAPI.Application.PublicTransport.Vehicles.Queries;

public class GetVehiclesLastSnapshotQuery : IRequest<List<VehicleDto>> {}

public class GetVehiclesLastSnapshotQueryHandler(
        VehicleSnapshotRepository vehicleSnapshotRepository,
        IMapper mapper,
        ILogger<GetVehiclesLastSnapshotQueryHandler> logger
        ) : IRequestHandler<GetVehiclesLastSnapshotQuery, List<VehicleDto>>
{
    public async Task<List<VehicleDto>> Handle(GetVehiclesLastSnapshotQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var vehicleDocuments = await vehicleSnapshotRepository.GetLastSnapshotAsync();
            return mapper.Map<List<VehicleDto>>(vehicleDocuments);
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            throw;
        }
        
    }
}