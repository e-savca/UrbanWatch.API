using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrbanWatchAPI.Application.PublicTransport.Vehicles.Commands;
using UrbanWatchAPI.Application.PublicTransport.Vehicles.DTOs;

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
    public IActionResult Get()
    {
        return Ok();
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
}