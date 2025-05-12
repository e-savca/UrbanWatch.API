using MediatR;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;
using UrbanWatchAPI.Infrastructure.Mongo.Repositories;

namespace UrbanWatchAPI.Application.PublicTransport.Vehicles.Commands;

public class DeleteVehiclesCommand : IRequest<int>
{
    public TimeSpan TimeSpan { get; set; }
}

public class DeleteVehiclesCommandHandler(
        VehicleSnapshotRepository vehicleSnapshotRepository
    ) : IRequestHandler<DeleteVehiclesCommand, int>
{
    public async Task<int> Handle(DeleteVehiclesCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await vehicleSnapshotRepository.ClearAsync(request.TimeSpan, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
