using MediatR;
using UrbanWatchAPI.Application.PublicTransport.Vehicles.DTOs;

namespace UrbanWatchAPI.Application.PublicTransport.Vehicles.Commands;

public class ImportVehiclesCommand : IRequest<Guid>
{
    public VehicleSnapshotDto? Snapshot { get; set; }
}

public class ImportVehiclesCommandHandler : IRequestHandler<ImportVehiclesCommand, Guid>
{
    public Task<Guid> Handle(ImportVehiclesCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}