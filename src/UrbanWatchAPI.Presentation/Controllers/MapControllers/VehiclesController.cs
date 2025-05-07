using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrbanWatchAPI.Application.PublicTransport.Vehicles.Commands;
using UrbanWatchAPI.Application.PublicTransport.Vehicles.DTOs;

namespace UrbanWatchAPI.Presentation.Controllers.MapControllers;

[ApiController]
[Tags("Map Controllers")]
[Route("map/[controller]")]
public class VehiclesController(
    IMediator mediator
    ) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post([FromBody] VehicleSnapshotDto vehicles)
    {
        var importCommand = new ImportVehiclesCommand()
        {
            Snapshot = vehicles
        };
        
        var result = mediator.Send(importCommand);
        
        return NoContent();
    }
}