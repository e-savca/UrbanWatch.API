using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using UrbanWatchAPI.Application.PublicTransport.Routes.DTOs;
using UrbanWatchAPI.Domain.Common.Exceptions;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;
using UrbanWatchAPI.Infrastructure.Mongo.Repositories;

namespace UrbanWatchAPI.Application.PublicTransport.Routes.Commands;

public class ImportRoutesCommand : IRequest<int>
{
    public List<RouteDTO> Routes { get; set; }
}
public class ImportRoutesCommandHandler(
    RouteRepository routeRepository,
    IMapper mapper,
    ILogger<ImportRoutesCommandHandler> logger
) : IRequestHandler<ImportRoutesCommand, int>
{
    public async Task<int> Handle(ImportRoutesCommand request, CancellationToken cancellationToken)
    {
        var oldRoutes = await routeRepository.GetAllAsync();
        var routeDocuments = mapper.Map<List<RouteDocument>>(request.Routes);

        try
        {
            logger.LogInformation("Importing {Count} new routes", routeDocuments.Count);

            await routeRepository.DeleteAllAsync();
            logger.LogInformation("All old routes deleted successfully.");

            var result = await routeRepository.InsertBatchAsync(routeDocuments);
            logger.LogInformation("Inserted {Count} new routes successfully", result.Count);

            return result.Count;
        }
        catch (MongoDeleteException ex)
        {
            logger.LogError(ex, "Failed to delete old routes. Import aborted.");
            throw;
        }
        catch (MongoInsertBatchException ex)
        {
            logger.LogError(ex, "Insert failed. Attempting to restore old routes...");

            try
            {
                await routeRepository.DeleteAllAsync(); 
                await routeRepository.InsertBatchAsync(oldRoutes);
                logger.LogWarning("Old routes successfully restored after failed insert.");
            }
            catch (Exception restoreEx)
            {
                logger.LogCritical(restoreEx, "Failed to restore old routes after insert failure.");
            }

            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error during route import.");
            throw;
        }
    }
}
