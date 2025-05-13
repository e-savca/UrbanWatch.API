using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrbanWatchAPI.Application.PublicTransport.Routes.Commands;
using UrbanWatchAPI.Application.PublicTransport.Routes.DTOs;
using UrbanWatchAPI.Application.PublicTransport.Routes.Queries.GetAllRoutes;

namespace UrbanWatchAPI.Presentation.Controllers.MapControllers;

[ApiController]
[Tags("Map Controllers")]
[Route("map/[controller]")]
public class RoutesController(
    IMediator mediator,
    ILogger<RoutesController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllRoutes()
    {
        var query = new GetAllRoutesQuery();
        var routes = await mediator.Send(query);

        // TODO: return list of routes from MongoDB
        return Ok(new
        {
            message = "List of all routes", count = routes.Count.ToString(), routes
        });
    }

    [HttpGet("{routeId:int}")]
    public IActionResult GetRouteById(int routeId)
    {
        // TODO: return specific route by ID from MongoDB
        return Ok(new { message = $"Details for route {routeId}" });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRoutes([FromBody] List<RouteDTO> routes)
    {
        if (routes == null || routes.Count == 0)
            return BadRequest("Routes list cannot be empty.");

        var importCommand = new ImportRoutesCommand()
        {
            Routes = routes
        };


        try
        {
            var result = await mediator.Send(importCommand);
            return Ok(new { status = "success" });
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            throw;
        }
    }
}