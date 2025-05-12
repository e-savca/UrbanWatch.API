using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using UrbanWatchAPI.Application.PublicTransport.Vehicles.DTOs;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;
using UrbanWatchAPI.Infrastructure.Mongo.Repositories;

namespace UrbanWatchAPI.Application.PublicTransport.Vehicles.Commands;

public class ImportVehiclesCommand : IRequest<Guid>
{
    public VehicleSnapshotDto? Snapshot { get; set; }
}

public class ImportVehiclesCommandHandler(
    IMapper mapper,
    VehicleSnapshotRepository vehicleSnapshotRepository,
    ILogger<ImportVehiclesCommandHandler> logger
    ) : IRequestHandler<ImportVehiclesCommand, Guid>
{
    public async Task<Guid> Handle(ImportVehiclesCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var snapshotDocument = mapper.Map<VehicleSnapshotDocument>(request.Snapshot);
            await vehicleSnapshotRepository.InsertAsync(snapshotDocument);

            return snapshotDocument.Id;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            throw;
        }
    }
}