using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrbanWatchAPI.Application.PublicTransport.Vehicles.Commands;
using UrbanWatchAPI.Application.PublicTransport.Vehicles.DTOs;
using UrbanWatchAPI.Application.PublicTransport.Vehicles.Queries;

namespace UrbanWatchAPI.Presentation.Controllers.MapControllers;

[ApiController]
[Tags("Map Controllers")]
[Route("map/[controller]")]
public class VehiclesController(
    IMediator mediator,
    ILogger<VehiclesController> logger
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var getVehiclesLastSnapshot = new GetVehiclesLastSnapshotQuery();

        try
        {
            var result = await mediator.Send(getVehiclesLastSnapshot);
            return Ok(result);
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] VehicleSnapshotDto vehicles)
    {
        var importCommand = new ImportVehiclesCommand()
        {
            Snapshot = vehicles
        };

        try
        {
            await mediator.Send(importCommand);

            return Created();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return StatusCode(500);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] long ticks)
    {
        var timespan = TimeSpan.FromTicks(ticks);

        var deleteCommand = new DeleteVehiclesCommand
        {
            TimeSpan = timespan
        };

        try
        {
            var result = await mediator.Send(deleteCommand);
            if (result > 0)
                return new JsonResult(new { success = true, deletedCount = result });
            
            return new JsonResult(new { success = false});
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to delete vehicles older than the given timespan.");
            return StatusCode(500);
        }
    }
}